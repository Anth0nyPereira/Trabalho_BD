using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets_At_First_Sight
{
    class Produto
    {
        private int _ID;
        private String _NomeProduto;
        private String _TipoServico;
        private String _Preco;
		private String _Empresa;
		private String _uImage;
		private int _Stock;

		public int ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		public String NomeProduto
		{
			get { return _NomeProduto; }
			set { _NomeProduto = value; }
		}
		public String TipoServico
		{
			get { return _TipoServico; }
			set { _TipoServico = value; }
		}

		public String Preco
		{
			get { return _Preco; }
			set { _Preco = value; }
		}

		public String Empresa
		{
			get { return _Empresa; }
			set { _Empresa = value; }
		}
		public String uImage
		{
			get { return _uImage; }
			set { _uImage = value; }
		}

		public int Stock
		{
			get { return _Stock; }
			set { _Stock = value; }
		}
	}
}
