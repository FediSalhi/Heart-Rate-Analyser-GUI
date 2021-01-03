using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heart_Rate_Analyser
{
    public enum Packet_capturing_status_e
    {
        CAPTURING_SYNCHRONOUS_1,
        CAPTURING_SYNCHRONOUS_2,
        CAPTURING_SOURCE_DEVICE,
        CAPTURING_TARGET_DEVICE,
        CAPTURING_PACKET_TYPE,
        CAPTURING_DATA_LENGTH,
        CAPTURING_BYTE1,
        CAPTURING_BYTE2,
        CAPTURING_BYTE3,
        CAPTURING_BYTE4,
        CAPTURING_BYTE5,
        CAPTURING_BYTE6,
        CAPTURING_BYTE7,
        CAPTURING_BYTE8
    }

    public enum Packet_types_e
    {
        HEART_RATE_MEASUREMENT_PACKET
    }

    public struct Simple_communication_packet_t
    {
        public byte synchronous_1_u8;
        public byte synchronous_2_u8;
        public byte source_device_u8;
        public byte target_device_u8;
        public byte paket_type_u8;
        public byte data_length_u8;
        public byte[] data_u8;
    }

    public struct Serial_uart_channel_t
    {
        public Packet_capturing_status_e paket_capturing_status_t;
        public short captured_packet_number_u16;
        public short solved_packet_number_u16;
        public Simple_communication_packet_t simple_com_packet_t;
        //void (*packet_solver)(Simple_communication_packet_t*); implement packet_solver
    }

    public class Simple_Communication
    {

        public const byte SYNCHRONOUS1 = 0x11;
        public const byte SYNCHRONOUS2 = 0x99;
        public const long MAX_DATA_LENGTH = 20000;
        public const byte PC = 0x00;
        public const byte MSP430 = 0x01;
        public const byte HEART_RATE_MEASUREMENT_PACKET = 0x00;

        public static short control_if_there_is_data_to_capture()
        {
            short data_received_s16 = 0;
            if (Global_Variables.GL_buffer_last_u32 == Global_Variables.GL_buffer_first_u32)
            {
                data_received_s16 = -1;
            }
            else if (Global_Variables.GL_buffer_last_u32 < Global_Variables.GL_buffer_first_u32) 
            {
                data_received_s16 = (short)(Global_Variables.GL_buffer_last_u32 - Global_Variables.GL_buffer_first_u32 + Simple_Communication.MAX_DATA_LENGTH);
            }
            else
            {
                data_received_s16 = (short)(Global_Variables.GL_buffer_last_u32 - Global_Variables.GL_buffer_first_u32);
            }
            return data_received_s16;
        }

        public static void capture_packet_from_bytes(short number_of_recieved_bytes, byte[] data_read_from_buffer)
        {
            ushort byte_index_u16 = 0;
            ushort data_index_u16 = 0;
            bool all_bytes_are_read = false;
         /* buffer is not empty */

            for (byte_index_u16 = 0; byte_index_u16 < number_of_recieved_bytes; byte_index_u16++)
            {
                switch (Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t)
                {
                    case Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1:
                    {
                    if (data_read_from_buffer[byte_index_u16] == SYNCHRONOUS1)
                    {
                        /* recived byte is synchronous 1 */
                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.synchronous_1_u8 = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_2;

                    }
                    break;
                    }

                    case Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_2:
                    {
                        if (data_read_from_buffer[byte_index_u16] == SYNCHRONOUS2)
                        {
                        /* recived byte is synchronous 1 */
                            Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.synchronous_2_u8 = data_read_from_buffer[byte_index_u16];
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SOURCE_DEVICE;

                        }
                        else
                        {
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                        }
                        break;
                    }

                    case Packet_capturing_status_e.CAPTURING_SOURCE_DEVICE:
                    {
                        if (data_read_from_buffer[byte_index_u16]== MSP430)
                        {
                       /* recived byte is synchronous 1 */
                            Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.source_device_u8 = data_read_from_buffer[byte_index_u16]; ;
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_TARGET_DEVICE;
                    }
                        else
                        {
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                        }
                        break;
                    }

                    case Packet_capturing_status_e.CAPTURING_TARGET_DEVICE:
                    {
                        if (data_read_from_buffer[byte_index_u16] == PC)
                        {
                       /* recived byte is synchronous 1 */
                            Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.source_device_u8 = data_read_from_buffer[byte_index_u16];
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_PACKET_TYPE;
                        }
                        else
                        {
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                        }
                        break;
                    }

                    case Packet_capturing_status_e.CAPTURING_PACKET_TYPE:
                    {
                        if (data_read_from_buffer[byte_index_u16] == HEART_RATE_MEASUREMENT_PACKET)
                        {
                       /* recived byte is synchronous 1 */
                            Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.paket_type_u8 = data_read_from_buffer[byte_index_u16]; 
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_DATA_LENGTH;
                        }
                        else
                        {
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                        }
                        break;
                        }

                    case Packet_capturing_status_e.CAPTURING_DATA_LENGTH:
                    {
                        if (data_read_from_buffer[byte_index_u16] == 8)
                        {
                            Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_length_u8 = data_read_from_buffer[byte_index_u16];
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE1;
                        }
                        else
                        {
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                        }
                        break;
                    }

                    case Packet_capturing_status_e.CAPTURING_BYTE1:
                    {   

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[0] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE2;
                        break;
                    }
                    case Packet_capturing_status_e.CAPTURING_BYTE2:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[1] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE3;
                        break;
                    }
                    
                    case Packet_capturing_status_e.CAPTURING_BYTE3:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[2] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE4;
                        break;
                    }
                        
                    case Packet_capturing_status_e.CAPTURING_BYTE4:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[3] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE5;
                        break;
                    }
                    
                    case Packet_capturing_status_e.CAPTURING_BYTE5:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[4] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE6;
                        break;
                    }
                        
                    case Packet_capturing_status_e.CAPTURING_BYTE6:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[5] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE7;
                        break;
                    }
                    
                    case Packet_capturing_status_e.CAPTURING_BYTE7:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[6] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_BYTE8;
                        break;
                    }
                        
                    case Packet_capturing_status_e.CAPTURING_BYTE8:
                    {

                        Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[7] = data_read_from_buffer[byte_index_u16];
                        Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                        solve_packet();
                        break;
                    }
                }
            }
            
        }

        public static void solve_packet()
        {
            combine_float64(Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8);
        }

        public static void get_data_from_buffer(byte[] data_u8, short number_of_bytes)
        {
            int byte_index = 0;
            for (byte_index = 0; byte_index < number_of_bytes; byte_index++)
            {
                data_u8[byte_index] = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                Global_Variables.GL_buffer_first_u32++;
                if (Global_Variables.GL_buffer_first_u32 >= Simple_Communication.MAX_DATA_LENGTH)
                {
                    Global_Variables.GL_buffer_first_u32 = 0;
                }
            }
        }

        public static void combine_float64(byte[] data)
        {
            Global_Variables.GL_sensor_measurement = System.BitConverter.ToDouble(data, 0);

            Global_Variables.GL_rx_measurement_u64[Global_Variables.GL_measurements_last_u32] = Global_Variables.GL_sensor_measurement;
            Global_Variables.GL_measurements_last_u32++;

            Console.WriteLine(Global_Variables.GL_measurements_last_u32);

            if (Global_Variables.GL_measurements_last_u32 >= 1000)
            {
                
                Global_Variables.GL_measurements_last_u32 = 0;
                Artificial_Intelligence.diagnostig_started = true;
            }

        }

        public static void communication_loop()
        {
            short number_of_bytes_received_s16 = 0;

            number_of_bytes_received_s16 = Simple_Communication.control_if_there_is_data_to_capture();

            if (number_of_bytes_received_s16 > 0)
            {
                byte[] data_from_receive_buffer = new byte[number_of_bytes_received_s16];

                //read data from global receive buffer
                Simple_Communication.get_data_from_buffer(data_from_receive_buffer, number_of_bytes_received_s16);
                //capture packet from read data
                Simple_Communication.capture_packet_from_bytes(number_of_bytes_received_s16, data_from_receive_buffer);

            }

            else
            {
                Console.WriteLine(number_of_bytes_received_s16);
            }
        }
    }
}
