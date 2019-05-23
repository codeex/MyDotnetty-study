/*****************************************************************
 * Copyright (C) 2019 上海科箭软件科技有限公司版权所有。 
 *
 * 文件名称    ：TimeServer  
 * 创 建 者    ：Mark
 * 创建日期    ：2019/5/22 星期三 18:20:28 
 *
 * 功能描述：  
******************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace SvrDemo
{
    public class TimeServer
    {
        public void Bind(int port)
        {
            //配置服务端线程组
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup();
            IEventLoopGroup workerGroup = new MultithreadEventLoopGroup();
            try
            {
                ServerBootstrap b = new ServerBootstrap();
                b.Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 1024)
                    .ChildHandler(new ChildChannelHandler());
                //绑定端口，同步等待成功
                var f = b.BindAsync(port).Result;

                Console.ReadKey();
                //等待端口关闭
                f.CloseAsync().Wait();
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            finally
            {
                //优雅退出，释放线程池资源
                bossGroup.ShutdownGracefullyAsync();
                workerGroup.ShutdownGracefullyAsync();
            }
        }

        private class ChildChannelHandler : ChannelInitializer<TcpSocketChannel>
        {
            protected override void InitChannel(TcpSocketChannel channel)
            {
                channel.Pipeline.AddLast(new TimeServerHandler());
            }
        }
    }
}
