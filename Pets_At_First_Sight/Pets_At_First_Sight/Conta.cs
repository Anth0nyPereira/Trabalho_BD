using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets_At_First_Sight
{
    class Conta
    {
        private String _Email;
        private String _Username;
        private String _NomePessoa;
        private String _Pass;
        private String _Localidade;
        private String _TipoConta;
        private String _Foto;

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public String Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public String NomePessoa
        {
            get { return _NomePessoa; }
            set { _NomePessoa = value; }
        }

        public String Pass
        {
            get { return _Pass; }
            set { _Pass = value; }
        }

        public String Localidade
        {
            get { return _Localidade; }
            set { _Localidade = value; }
        }

        public String TipoConta
        {
            get { return _TipoConta; }
            set { _TipoConta = value; }
        }

        public String Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }


    }

    

}
