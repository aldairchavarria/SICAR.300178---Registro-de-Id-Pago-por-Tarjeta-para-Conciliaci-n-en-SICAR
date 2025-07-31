using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace COM_SIC_Log4Net
{
    public class Globales
    {
        private string _NombreMaquina;
        private string _matriculaUsuario;
        private string _logon_user;
        private string _IPMaquina;
        private string _CodigoAplicativo;
        private string _NombreProyecto;

        public string NombreMaquina
        {
            get { return _NombreMaquina; }
            set { _NombreMaquina = value; }
        }
        public string LogonUser
        {
            get { return _logon_user; }
            set { _logon_user = value; }
        }
        public string IPMaquina
        {
            get { return _IPMaquina; }
            set { _IPMaquina = value; }
        }
        public string MatriculaUsuario
        {
            get { return _matriculaUsuario; }
            set { _matriculaUsuario = value; }
        }
        public string CodigoAplicativo
        {
            get { return _CodigoAplicativo; }
            set { _CodigoAplicativo = value; }
        }
        public string NombreProyecto
        {
            get { return _NombreProyecto; }
            set { _NombreProyecto = value; }
        }

        public Globales()
        {
            _NombreMaquina = System.Net.Dns.GetHostName();
            _CodigoAplicativo = "CodigoAplicativo";
            _NombreProyecto ="NombreProyecto";
            _matriculaUsuario = System.Environment.UserName;
            _IPMaquina = System.Net.Dns.GetHostByName(_NombreMaquina).AddressList[0].ToString();
            _logon_user = System.Environment.UserDomainName + "/" + System.Environment.UserName;
        }
    }

}
