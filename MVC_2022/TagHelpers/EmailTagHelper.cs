using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC_2022.TagHelpers
{
    //Cria classe herdando de TagHelper do aspnet core.
    public class EmailTagHelper : TagHelper
    {
        public string Endereco { get; set; }
        public string Conteudo { get; set; }

        //Sobreescreve método process de TagHelper do asp net core
        //Montaremos um link para o e-mail, com as propriedades como valores.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href","mailto:" + Endereco);  
            output.Content.SetContent(Conteudo);
        }
    }
}
