using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heart_Rate_Analyser
{

    public class Global_Variables
    {
        public static byte[] GL_rx_buffer_u8 = new byte[Simple_Communication.MAX_DATA_LENGTH];       
        public static uint GL_buffer_first_u32 = 0; /* using static to get this variable from another class without creating an instance */
        public static uint GL_buffer_last_u32  = 0;

        public static Serial_uart_channel_t GL_serial_uart_channel_t;
        


        public static void init_serial_uart_channel()
        {
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8 = new byte[8];
            GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
            GL_serial_uart_channel_t.captured_packet_number_u16 = 0;
            GL_serial_uart_channel_t.solved_packet_number_u16 = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[0] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[1] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[2] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[3] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[4] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[5] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[6] = 0;
            GL_serial_uart_channel_t.simple_com_packet_t.data_u8[7] = 0;



        }


    }




 
}
