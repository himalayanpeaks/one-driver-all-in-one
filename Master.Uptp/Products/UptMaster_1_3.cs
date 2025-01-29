using OneDriver.Framework.Libs.Announcer;
using OneDriver.Framework.Libs.Validator;
using OneDriver.Framework.Module;
using System.Runtime.InteropServices;
using Serilog;
using static OneDriver.Master.Uptp.Products.Definition;
using OneDriver.Toolbox;
using System.Diagnostics;

namespace OneDriver.Master.Uptp.Products
{
    public class UptMaster_1_3 : DataTunnel<InternalDataHAL>, IUptHAL
    {
        public Definition.e_slave_com_port SensorPortNumber { get; set; } = Definition.e_slave_com_port.COMMUNICATION_PORT_1;

        public UptMaster_1_3()
        {
            NumberOfChannels = 2;
        }
        public ConnectionError Open(string initString, IValidator validator)
        {

            string comport = validator.ValidationRegex.Match(initString).Groups[1].Value;
            var err = SSPP_Link(comport, e_baudrates.BAUDRATE_230400, e_com_state.CONNECT);
            if (err != e_error_codes.ERR_NONE)
            {
                Log.Error("UPT master - PC connection error: " + err + ", error code " + Convert.ToInt16(err));
                return ConnectionError.CommunicaionError;
            }
            else
            {
                Log.Information("UPT connected to PC");
                StartProcessDataAnnouncer();
            }

            return ConnectionError.NoError;
        }

        public ConnectionError Close()
        {
            var err = SSPP_Link("", e_baudrates.BAUDRATE_230400, e_com_state.DISCONNECT);
            if (err != e_error_codes.ERR_NONE)
            {
                Log.Error("UPT master - PC disconnect error: " + err + ", error code " + Convert.ToInt16(err));
                return ConnectionError.CommunicaionError;
            }
            else
            {
                Log.Information("UPT master disconnected from PC");
                StopProcessDataAnnouncer();
            }

            return ConnectionError.NoError;
        }

        public void StartProcessDataAnnouncer() => StartAnnouncingData();

        public void StopProcessDataAnnouncer() => StopAnnouncingData();


        public void AttachToProcessDataEvent(DataTunnel<InternalDataHAL>.DataEventHandler processDataEventHandler) => DataEvent += processDataEventHandler;

        public int NumberOfChannels { get; }

        #region DllImport

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes SSPP_Link(string aComPort, e_baudrates aBaudrate, e_com_state aComState);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern e_error_codes UPTP_Link(e_activeprot aActivePort, string aPort, e_baudrates aBaudRate,
            e_com_state aComState);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr UPTP_GetCOMPort();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr UPTP_GetVersionID();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_com_state UPTP_GetComState();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes UPTP_RdID(ref s_uptp_resp aResponse, byte index);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes UPTP_RdStat(ref s_uptp_resp aResponse, byte aRequest);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern byte set_slave_configuration(e_slave_output_type aOutputType, e_slave_com_port aComPort,
            e_connection_type aConnectionType, byte aThresholdVoltage);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void set_sspp_slave_timeout(ushort aTimeout);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort get_sspp_slave_timeout();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void set_uptp_timeout(ushort aTimeout);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort get_uptp_timeout();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void set_sspp_slave_retrials(byte aRetrials);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort get_sspp_slave_retrials();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void set_uptp_idle_poll_time(ushort aRetrials);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort get_uptp_idle_poll_time();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SSPP_GetVersionID();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SSPP_GetProtocolID();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr UPTP_GetProtVer();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr UPTP_GetProtocolID();

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes SSPP_Idle(ref s_sspp_resp aSsppResponse);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes SSPP_RdPar(ref s_sspp_resp aResponse, ushort index,
            e_sspp_par_access aParamAccess);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes SSPP_WrPar(ref s_sspp_resp aResponse, ushort index,
            e_sspp_par_access aParamAccess, byte[] aData, byte aSizeOfData);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes UPTP_WrCtrl(ref s_uptp_resp aResponse, byte aRequestCode, byte[] aRequestData);

        [DllImport(@"sspp_master_api.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern e_error_codes SSPP_WrCmd(ref s_sspp_resp aResponse, ushort index, ref byte[] aRequestData,
            byte aSize);

        #endregion DllImport
        private ushort _processDataIndex { get; set; }
        public e_error_codes SetProcessData(ushort index, out int lengthInBytes)
        {
            var err = ReadParam(index, out var readBuffer);
            lengthInBytes = 0;
            if (err == e_error_codes.ERR_NONE)
            {
                _processDataIndex = index;
                if (readBuffer != null) lengthInBytes = readBuffer.Length;
            }
            else
                lengthInBytes = 0;
            return err;
        }

        public e_error_codes WriteCommand(e_sspp_cmds command, ref byte[] aRequestData)
        {
            var sSsppResp = new s_sspp_resp();
            return SSPP_WrCmd(ref sSsppResp, (ushort)command, ref aRequestData, (byte)aRequestData.Length);
        }

        public e_error_codes PowerOff()
        {
            e_error_codes err = e_error_codes.ERR_NONE;
            Definition.s_uptp_resp uptp_response = new Definition.s_uptp_resp();
            Byte[] uptp_request_data = new Byte[1];
            uptp_request_data[0] = (Byte)(Definition.e_intf_sply_on_state.INTF_SUPPLY_OFF);
            err = UPTP_WrCtrl(ref uptp_response, (Byte)Definition.e_uptp_req.UPTP_REQ_INTF_SPLY, uptp_request_data);
            if (err != e_error_codes.ERR_NONE)
                Log.Error("Error in Power OFF: " + err.ToString());
            return err;
        }

        public e_error_codes PowerOn()
        {
            e_error_codes err = e_error_codes.ERR_NONE;
            Definition.s_uptp_resp uptp_response = new Definition.s_uptp_resp();
            Byte[] delay = { 136, 19 };
            err = UPTP_WrCtrl(ref uptp_response, (Byte)Definition.e_uptp_req.UPTP_REQ_SPLY_ON_COM, delay);
            if (err != e_error_codes.ERR_NONE)
                Log.Error("Error in Power ON: " + err.ToString());
            return err;
        }

        protected override void FetchDataForTunnel(out InternalDataHAL data)
        {
            ReadParam(_processDataIndex, out var readBuffer);
            data = new InternalDataHAL((int)SensorPortNumber, _processDataIndex, readBuffer);
        }
        public e_error_codes ReadParam(ushort index, out byte[] data)
        {
            var response = new s_sspp_resp();
            var err = SSPP_RdPar(ref response, index, e_sspp_par_access.SSPP_PAR_INDEX_ACCESS);
            var dataNonNullable = new byte[response.obj_len];
            data = new byte[response.obj_len];
            if (err != e_error_codes.ERR_NONE)
            {
                Log.Information("Could not read index " + index);
                return err;
            }

            try
            {
                if (response.obj_len > 0)
                {
                    Marshal.Copy(response.p_obj_data, dataNonNullable, 0, response.obj_len);
                    err = GetErrorCode(response);
                }
            }

            catch (Exception e)
            {
                Log.Error("Error in reading index " + index + e.Message);
                return e_error_codes.RET_SDO_WRONG_BLOCKSIZE;
            }

            data = Array.ConvertAll<byte, byte>(dataNonNullable, input => input);
            return err;
        }
        private e_error_codes GetErrorCode(s_sspp_resp aResponse)
        {
            if (aResponse.error_code == e_error_codes.ERR_NONE)
                if ((aResponse.info.response_info & 8) >> 3 == 1)
                    if (aResponse.obj_len == 2)
                    {
                        var data = new byte[aResponse.obj_len];
                        Marshal.Copy(aResponse.p_obj_data, data, 0, aResponse.obj_len);
                        aResponse.error_code = (e_error_codes)((data[0] << 8) | data[1]);
                    }
                    else
                    {
                        aResponse.error_code = e_error_codes.ERR_UNDEF;
                    }

            return aResponse.error_code;
        }
        public e_error_codes WriteParam(ushort index, byte[] data)
        {
            var response = new s_sspp_resp();

            var err = SSPP_WrPar(ref response, index, e_sspp_par_access.SSPP_PAR_INDEX_ACCESS, data, (byte)data.Length);

            if (err == e_error_codes.ERR_NONE)
            {
                var returnedData = new byte[response.obj_len];
                if (response.obj_len > 0)
                {
                    Marshal.Copy(response.p_obj_data, returnedData, 0, response.obj_len);
                    err = GetErrorCode(response);
                }
            }

            return err;
        }
        public e_com ConnectSensorWithMaster()
        {
            var ssppResponse = new s_sspp_resp();
            DateTime start = DateTime.Now;
            var communicationStatus = e_com.COM_OFFLINE;

            var ret = set_slave_configuration(e_slave_output_type.OUTPUT_TYPE_PUSH_PULL, SensorPortNumber,
                e_connection_type.CONNECT_NOW, 60);
            Tools.Wait(100);

            if (Convert.ToBoolean(ret))
            {
                var uptpResponse = new s_uptp_resp();
                for (uint i = 0; i < 100; i += 2)
                {
                    UPTP_RdStat(ref uptpResponse, (byte)e_uptp_req.UPTP_REQ_COM);
                    var aData = new byte[uptpResponse.obj_len];
                    if (uptpResponse.obj_len > 0)
                        Marshal.Copy(uptpResponse.p_obj_data, aData, 0, uptpResponse.obj_len);
                    if (uptpResponse.obj_len == 1 && aData[0] == (byte)e_com.COM_ONLINE)
                    {
                        SSPP_Idle(ref ssppResponse);
                        var end = DateTime.Now;
                        Debug.WriteLine("duration for connect = " + (end - start).TotalSeconds);
                        Log.Information(e_com.COM_ONLINE + ". Time taken to connect " +
                                        (end - start).TotalSeconds);
                        ReadParam(5, out var _);
                        return e_com.COM_ONLINE;
                    }
                    communicationStatus = (e_com)aData[0];
                    Thread.Sleep(50);
                }
            }
            else
                return e_com.COM_ERROR;
            Log.Information("Could not connect to sensor");
            return communicationStatus;
        }

        public e_com DisconnectSensorFromMaster()
        {
            var request = Array.Empty<byte>();
            StopAnnouncingData();
            Thread.Sleep(50);
            WriteCommand(e_sspp_cmds.SSPP_RESET, ref request);
            var ret = set_slave_configuration(e_slave_output_type.OUTPUT_TYPE_PUSH_PULL, SensorPortNumber,
                e_connection_type.CONNECT_DISCARD, 60);
            if (Convert.ToBoolean(ret) == false)
                Log.Information("Could not disconnect");
            ret = set_slave_configuration(e_slave_output_type.OUTPUT_TYPE_PUSH_PULL, SensorPortNumber,
                e_connection_type.CONNECT_NOT, 60);
            if (Convert.ToBoolean(ret) == false)
                Log.Information("Could not disconnect");

            var uptpResponse = new s_uptp_resp();
            UPTP_RdStat(ref uptpResponse, (byte)e_uptp_req.UPTP_REQ_COM);
            var aData = new byte[uptpResponse.obj_len];
            if (uptpResponse.obj_len > 0)
                Marshal.Copy(uptpResponse.p_obj_data, aData, 0, uptpResponse.obj_len);
            if (uptpResponse.obj_len == 1 && aData[0] == (byte)e_com.COM_OFFLINE)
            {
                Log.Information(e_com.COM_OFFLINE + ". Sensor disconnected");
                return (e_com)aData[0];
            }
            Tools.Wait(200);
            return (e_com)aData[0];
        }
    }
}
