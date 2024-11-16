namespace Treino_REST_02.Models
{
    public class CEP
    {
        public string? Codigo {  get; set; }
        public string? Logradouro { get; set; }
        public string? LogradouroTipo { get; set; }
        public string? LogradouroNome {  get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? Estado { get; set; }
        public string? Regiao { get; set; }
        public string? DDD { get; set; }
    }

    public class ViaCEP
    {
        public string? cep { get; set; }
        public string? logradouro { get; set; }
        public string? complemento { get; set; }
        public string? unidade { get; set; }
        public string? bairro { get; set; }
        public string? localidade { get; set; }
        public string? uf { get; set; }
        public string? estado { get; set; }
        public string? regiao { get; set; }
        public string? ibge { get; set; }
        public string? gia { get; set; }
        public string? ddd { get; set; }
        public string? siafi { get; set; }

    }
}
