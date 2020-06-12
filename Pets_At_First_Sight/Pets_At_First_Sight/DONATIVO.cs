using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets_At_First_Sight
{
    class DONATIVO
    {
        private String _MeioPagamento;
        private System.Decimal _Quantia;
        private String _TipoAlimento;
        private String _QuantidadeAlimento;
        private String _Abrigo;

        public String MeioPagamento
        {
            get { return _MeioPagamento;}
            set { _MeioPagamento = value; }
        }
        
        public System.Decimal Quantia
        {
            get { return _Quantia;}
            set { _Quantia = value; }
        }
        
        public String TipoAlimento
        {
            get { return _TipoAlimento;}
            set { _TipoAlimento = value; }
        }
        
        public String QuantidadeAlimento
        {
            get { return _QuantidadeAlimento;}
            set { _QuantidadeAlimento = value; }
        }

        public String Abrigo
        {
            get { return _Abrigo; }
            set { _Abrigo = value; }
        }
    }
}
