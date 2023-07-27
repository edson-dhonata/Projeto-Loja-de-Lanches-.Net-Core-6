namespace MVC_2022.Models
{
    public class FileManagerModel
    {
        //Da acesso a métodos e propriedades para tratamento de imagens.
        public FileInfo[] Files { get; set; }

        //Interface que da acesso aos métodos de envio de imagens ao servidor, além de diversas informações do arquivo
        public IFormFile IFormFile { get; set; }
        public List<IFormFile> IFormFiles { get; set; }

        //Armazena nome da pasta do servidor.
        public string PathImagesProduto { get; set; }
    }
}
