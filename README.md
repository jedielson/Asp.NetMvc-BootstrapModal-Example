# BootstrapModal

Antes de mais nada, vamos aos devidos créditos:

Este exemplo é inteiramente baseado [neste post](http://www.devzest.com/blog/post/ASPNet-MVC-Modal-Dialog.aspx).
Seu repositório original está [aqui](https://mvcmodaldialog.codeplex.com/releases/view/106691)

Não, fazer um dialog popUp utilizando Jquery, ou Bootstrap não é algo realmente novo no mercado.
Na verdade, é possível ficar horas e horas navegando na web e obter um milhão de soluções diferentes.

Há algumas questões interessantes a serem resolvidas, como:

 - Como evitar que para n popups diferentes na solução, sejam implementadas n soluções diferentes, com n códigos Javascript diferentes?
 - Como garantir que seja possível integrar as facilidades do Asp.Net Mvc, como HtmlHelpers, e fazer isto sem corromper a estrutura do projeto, ou o padrão de desenvolvimento adotado?
 - Como retornar dados gerados no submit de um formulário aberto em um popup, de forma a customizada?
  
 
A parte boa, é que podemos resolver quase todas estas perguntas de forma elegante.

Então, vamos ao passo à passo para implementar esta solução no seu projeto.

 1. A solução é baseada no framework Asp.Net Mvc, e foi desenvolvida com a versão 5 deste framework.
 2. Você precisará instalar algumas libs neste projeto, a saber:

 > - [JQuery - v 2.1.4](https://www.nuget.org/packages/jQuery)
 > - [Bootstrap - v 3.3.5](https://www.nuget.org/packages/bootstrap)
 > - [JQuery Validation - v 1.8.1](https://www.nuget.org/packages/jQuery.Validation/1.8.1)
 > - [Microsoft JQuery Unobtrusive Validation - v 3.2.3](https://www.nuget.org/packages/Microsoft.jQuery.Unobtrusive.Validation/)
 > - [Microsoft jQuery Unobtrusive Ajax - v 3.2.3](https://www.nuget.org/packages/Microsoft.jQuery.Unobtrusive.Ajax/)

 3. Após todas as suas libs estarem instaladas, você precisará copiar os arquivos **ModalDialogExtensions.cs** e **modaldialog.js**. Você pode colocar o arquivo de extensões em uma pasta customizada do projeto, ou na raíz. Após isso, certifique-se de efetuar as correções de namespaces necessárias, se assim desejar.
 4. O passo seguinte é a configuração do uso do script, no arquivo **BundleConfig.cs**, como no exemplo abaixo:


```
bundles.Add(new ScriptBundle("~/bundles/modalPopUp").Include(
                    "~/Scripts/modaldialog.js"));
```
