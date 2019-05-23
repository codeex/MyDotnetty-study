/*****************************************************************
 * Copyright (C) 2019 上海科箭软件科技有限公司版权所有。 
 *
 * 文件名称    ：ClientHandler  
 * 创 建 者    ：Mark
 * 创建日期    ：2019/5/23 星期四 19:50:45 
 *
 * 功能描述：  
******************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace ClientDemo
{
    public class ClientHandler : ChannelInitializer<TcpSocketChannel>
    {
        protected override void InitChannel(TcpSocketChannel channel)
        {
            channel.Pipeline.AddLast(new TimeClientHandler());
        }
    }
}
