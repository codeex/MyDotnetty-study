/*****************************************************************
 * Copyright (C) 2019 上海科箭软件科技有限公司版权所有。 
 *
 * 文件名称    ：TimeClientHandler  
 * 创 建 者    ：Mark
 * 创建日期    ：2019/5/23 星期四 19:53:16 
 *
 * 功能描述：  
******************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace ClientDemo
{
    public class TimeClientHandler : ChannelHandlerAdapter
    {
        private IByteBuffer firstMessage;
        public TimeClientHandler()
        {
            byte[] req = Encoding.UTF8.GetBytes("query time");
            firstMessage = Unpooled.Buffer(req.Length);
            firstMessage.WriteBytes(req);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            context.WriteAndFlushAsync(firstMessage);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer buf = (IByteBuffer)message;
            byte[] req = new byte[buf.ReadableBytes];
            buf.ReadBytes(req);
            string body = Encoding.UTF8.GetString(req);
            Console.WriteLine($"now is {body}");
        }
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);
            context.CloseAsync();
        }
    }
}
