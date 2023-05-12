namespace AseregeBarcelonaWeb.Model.Data
{
    public class JavascriptUser : User
    {
        public Authorize authorize { get; set; }


        private char _Sexo;
        public string Sexo
        {
            get => _Sexo.ToString();
            set
            {
                _Sexo = value.ToCharArray()[0];
                if (!string.IsNullOrEmpty(value.ToString()))
                    base.Sexo = _Sexo;
            }
        }
    }
}
