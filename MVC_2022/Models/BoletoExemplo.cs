using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace MVC_2022.Models
{
    class BoletoExemplo
    {
        public async Task ConsultarBoleto()
        {
            // URL da API (coloque sua URL real aqui)
            string apiUrl = "https://exemplo.com/api";

            // Caminho para o arquivo PFX
            string certPath = "caminho/para/seu/certificado.pfx";

            // Senha do certificado PFX
            string certPassword = "teste";

            // Criando um objeto TicketRequest para representar o XML
            var ticketRequest = new TicketRequestData
            {
                Dados = new Dados
                {
                    Entry = new Entry[]
                    {
                        new Entry { Key = "CONVENIO.COD-BANCO", Value = "0033" },
                        new Entry { Key = "CONVENIO.COD-CONVENIO", Value = "123456789" },
                        // Adicione os outros campos aqui
                    },
                    Mensagem = "Teste de envio de boleto"
                },
                Expiracao = 100,
                Sistema = "YMB"
            };

            try
            {
                // Serializando o objeto TicketRequest em XML
                var xmlSerializer = new XmlSerializer(typeof(TicketRequest));
                var xmlString = SerializeToXmlString(xmlSerializer, ticketRequest);

                // Crie uma instância do certificado PFX
                X509Certificate2 certificate = new X509Certificate2(certPath, certPassword);

                // Crie uma instância HttpClientHandler e configure o certificado
                HttpClientHandler handler = new HttpClientHandler();
                handler.ClientCertificates.Add(certificate);

                // Crie uma instância HttpClient com o HttpClientHandler
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    // Configure os cabeçalhos ou outras informações da requisição, se necessário
                    // httpClient.DefaultRequestHeaders.Add("HeaderName", "HeaderValue");

                    // Crie o conteúdo da requisição com o XML
                    var content = new StringContent(xmlString, Encoding.UTF8, "text/xml");

                    // Envie a requisição POST com o XML no corpo
                    var response = await httpClient.PostAsync(apiUrl, content);

                    // Verifique o status da resposta
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Resposta da API:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Erro na requisição. Status Code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        
        // Função para serializar um objeto em uma string XML
        private static string SerializeToXmlString(XmlSerializer serializer, object obj)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(xmlWriter, obj);
                }
                return textWriter.ToString();
            }
        }

        [XmlRoot("soapenv:Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class TicketRequest
        {
            [XmlElement("soapenv:Body")]
            public TicketRequestBody Body { get; set; }
        }

        public class TicketRequestBody
        {
            [XmlElement("impl:create", Namespace = "http://impl.webservice.dl.app.bsbr.altec.com/")]
            public Create Create { get; set; }
        }

        public class Create
        {
            [XmlElement("TicketRequest")]
            public TicketRequestData TicketRequestData { get; set; }
        }

        public class TicketRequestData
        {
            public Dados Dados { get; set; }
            public int Expiracao { get; set; }
            public string Sistema { get; set; }
        }

        public class Dados
        {
            [XmlArray("entry")]
            public Entry[] Entry { get; set; }
            public string Mensagem { get; set; }
        }

        public class Entry
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
