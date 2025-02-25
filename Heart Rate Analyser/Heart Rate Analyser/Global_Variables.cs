﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heart_Rate_Analyser
{

    public class Global_Variables
    {
        /* ring buffer */
        public static byte[] GL_rx_buffer_u8 = new byte[Simple_Communication.MAX_DATA_LENGTH];       
        public static uint GL_buffer_first_u32 = 0; 
        public static uint GL_buffer_last_u32  = 0;

        /*  buffer to save measurements */
        public static double[] GL_rx_measurement_u64 = new double[1000];
        public static uint GL_measurements_last_u32 = 0;

        /* analysis result */
        public static string GL_analysis_result_string = "Result Is Not Ready ... Please Wait";


        /* serial channel */
        public static Serial_uart_channel_t GL_serial_uart_channel_t;

        /* sensor measurement */
        public static double GL_sensor_measurement = 0;

        /* init serial channel */
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
