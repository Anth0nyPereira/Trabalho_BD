using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets_At_First_Sight
{
    class ANIMAL
    {
		private int _Id;
        private String _Nome;
		private String _Especie;
		private String _Raca;
		private String _Idade;
        private String _Genero;
		private String _User_name;
		private String _Url_Image; //Comentar em BD
		private String _Tipo_Doador;
		private String _Vacinas;
		private String _Chip;
		private String _Mensagem;
		private bool _Adotado;
		private bool _Favorito;
		private String _ChangeAdoptIcon;
		private String _ChangeHeartIcon;

		public int Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		public String Nome
		{
			get { return _Nome; }
			set { _Nome = value; }
		}


		public String Idade
		{
			get { return _Idade; }
			set
			{
				_Idade = value;
			}
		}

		public String Genero
		{
			get { return _Genero; }
			set { _Genero = value; }
		}

		public String Especie
		{
			get { return _Especie; }
			set { _Especie = value; }
		}

		public String Raca
		{
			get { return _Raca; }
			set { _Raca = value; }
		}
		public String Url_Image
		{
			get { return _Url_Image; }
			set { _Url_Image = value; }
		}
		public String User_Name
		{
			get { return _User_name; }
			set {_User_name = value; }
		}

		public String Tipo_Doador
		{
			get { return _Tipo_Doador; }
			set { _Tipo_Doador = value; }
		}
		public String Vacinas
		{
			get { return _Vacinas; }
			set { _Vacinas = value; }
		}
		public String Chip
		{
			get { return _Chip; }
			set { _Chip = value; }
		}
		public String Mensagem
		{
			get { return _Mensagem; }
			set { _Mensagem = value; }
		}

		public bool Adotado
		{
			get { return _Adotado; }
			set { _Adotado = value; }
		}

		public bool Favorito
		{
			get { return _Favorito; }
			set { _Favorito = value; }
		}

		public String ChangeAdoptIcon
		{
			get{ if (_Adotado) { return "Star"; }
				else { return "StarOutline"; } }
		}

		public String ChangeFavIcon
		{
			get
			{
				if (_Favorito) { return "Heart"; }
				else { return "HeartOutline"; }
			}
		}

	}
}
