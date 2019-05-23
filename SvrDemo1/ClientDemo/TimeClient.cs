/*****************************************************************
 * Copyright (C) 2019 上海科箭软件科技有限公司版权所有。 
 *
 * 文件名称    ：TimeClient  
 * 创 建 者    ：Mark
 * 创建日期    ：2019/5/23 星期四 19:44:23 
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

namespace ClientDemo
{
    public class TimeClient
    {
        public void Connect(int port,string host)
        {
            IEventLoopGroup group = new MultithreadEventLoopGroup();
            try
            {
                Bootstrap b = new Bootstrap();
                b.Group(group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ClientHandler());
                var f = b.ConnectAsync(host, port).Result;
                Console.ReadKey();
                f.CloseAsync().Wait();
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            finally
            {
                group.ShutdownGracefullyAsync();
            }
        }
    }
}
