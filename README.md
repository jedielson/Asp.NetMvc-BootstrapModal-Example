# BootstrapModal

## Conteúdo
 - [Creditos](#antes-de-mais-nada-os-devidos-agradecimentos)
 - [Sobre Um Modal PopUp Funcional](#sobre-um-modal-popup-funcional)
 - [Configurando](#configurando)
 - [Como Utilizar](#como-utilizar)

### Antes de mais nada os devidos agradecimentos

Este exemplo é inteiramente baseado [neste post](http://www.devzest.com/blog/post/ASPNet-MVC-Modal-Dialog.aspx).
Seu repositório original está [aqui](https://mvcmodaldialog.codeplex.com/releases/view/106691).

A diferença básica, é que esta implementação é baseada em Bootstrap, e a primeira em JQuery UI.

De qualquer forma, meu muito obrigado ao autor!

### Sobre Um Modal PopUp Funcional

Não, fazer um dialog popUp utilizando Jquery, ou Bootstrap não é algo realmente novo no mercado.
Na verdade, é possível ficar horas e horas navegando na web e obter um milhão de soluções diferentes.

Há algumas questões interessantes a serem resolvidas, como:

 - Como evitar que para n popups diferentes na solução, sejam implementadas n soluções diferentes, com n códigos Javascript diferentes?
 - Como garantir que seja possível integrar as facilidades do Asp.Net Mvc, como HtmlHelpers, e fazer isto sem corromper a estrutura do projeto, ou o padrão de desenvolvimento adotado?
 - Como retornar dados gerados no submit de um formulário aberto em um popup, de forma a customizada?
  
 
A parte boa, é que podemos resolver quase todas estas perguntas de forma elegante.

### Configurando

Então, vamos ao passo à passo para implementar esta solução no seu projeto.

 1. A solução é baseada no framework Asp.Net Mvc, e foi desenvolvida com a versão 5 deste framework.
 2. Você precisará instalar algumas libs neste projeto, a saber:

 > - [JQuery - v 2.1.4](https://www.nuget.org/packages/jQuery)
 > - [Bootstrap - v 3.3.5](https://www.nuget.org/packages/bootstrap)
 > - [JQuery Validation - v 1.8.1](https://www.nuget.org/packages/jQuery.Validation/1.8.1)
 > - [Microsoft JQuery Unobtrusive Validation - v 3.2.3](https://www.nuget.org/packages/Microsoft.jQuery.Unobtrusive.Validation/)
 > - [Microsoft jQuery Unobtrusive Ajax - v 3.2.3](https://www.nuget.org/packages/Microsoft.jQuery.Unobtrusive.Ajax/)

 3. Após todas as suas libs estarem instaladas, você precisará copiar os arquivos **ModalDialogExtensions.cs** e **modaldialog.js**. Você pode colocar o arquivo de extensões em uma pasta customizada do projeto, ou na raíz. Após isso, certifique-se de efetuar as correções de namespaces necessárias, se assim desejar.
 
 > <i class="icon-cog"></i>Para o correto funcionamento dos HtmlHelpers, você deve entrar no arquivo Web.config da pasta ~/Views, e adicionar o namespace do arquivo **ModalDialogExtensions.cs**
 > ``` xml
 > <system.web.webPages.razor>
     <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.0.0.0, Culture=neutral, PublicKeyToken=blahblahtoken" />
     <pages pageBaseType="System.Web.Mvc.WebViewPage">
       <namespaces>
         <add namespace="System.Web.Mvc" />
         <add namespace="System.Web.Mvc.Ajax" />
         <add namespace="System.Web.Mvc.Html" />
         <add namespace="System.Web.Optimization"/>
         <add namespace="System.Web.Routing" />
         <add namespace="namespace.of.my.modaldialogextensions" />
       </namespaces>
     </pages>
   </system.web.webPages.razor>
 > ```

 4. O passo seguinte é a configuração do uso do script, no arquivo **BundleConfig.cs**, como no exemplo abaixo:

 > ``` c#
 > bundles.Add(new ScriptBundle("~/bundles/modalPopUp").Include(
 >                     "~/Scripts/modaldialog.js"));
 > ```
 
 5. Agora, basta configurar a renderização dos scripts no layout.

 > ``` C#
 > @Scripts.Render("~/bundles/jquery")
 > @Scripts.Render("~/bundles/jqueryval")
 > @Scripts.Render("~/bundles/bootstrap")
 > @Scripts.Render("~/bundles/modalPopUp")
 > @RenderSection("scripts", required: false)
 > ```

### Como Utilizar

A ideia da solução, é configurar onde e como será usado um PopUp, através de HtmlHelpers, afim de obter uma implantação padronizada da solução.

#### 1 - Configurando a chamada do PopUp ####

Para criar um link capaz de criar um PopUp, basta usar o AjaxHelper ModalDialogActionLink.

  > ``` C#
  > @Ajax.ModalDialogActionLink("Texto do Link", "Action", "Controller", "Título da Janela", RouteValues, htmlAttributes, "funcaoCallback")
  > ```

Este helper, renderiza um link com as informações básicas para a criação do PopUp.

 > ``` html
 > <a data-ajax="true" 
      data-ajax-begin="prepareModalDialog('d6f58bd7-5747-4346-8aa1-3048e94d4969')
      data-ajax-failure="clearModalDialog('d6f58bd7-5747-4346-8aa1-3048e94d4969');alert('Ajax call failed')"
      data-ajax-method="GET" data-ajax-mode="replace"
      data-ajax-success="openModalDialog('d6f58bd7-5747-4346-8aa1-3048e94d4969', 'Título da Janela', funcaoCallback)"
      data-ajax-update="#d6f58bd7-5747-4346-8aa1-3048e94d4969" 
      href="/Controller/Action">
      Texto do Link
    </a>
 > ```

#### 2 - Configurando o retorno do PopUp ####

Após efetuar o submit do dos dados contidos no formulário, você pode tratar a resposta de três formas diferentes.

- Atualizando o formulário para exibir erros, usando os recursos de validação do Asp.Net Mvc
- Empilhando outro PopUp
- Fechando o PopUp, podendo enviar dados e tratá-los usando a funcção de callback informada anteriormente.
 
Como exemplo, podemos usar o código abaixo:

> ``` C#
 ActionResult ProcessDialog(DialogModel model, int answer, string message)
        {
            if (this.ModelState.IsValid)
            {
                var data = new { id = answer, valor = "Mensagem " + answer };
                if (model.Value == answer)
                {
                    return this.DialogResult(message, data); // Close dialog via DialogResult call
                }
                this.ModelState.AddModelError(string.Empty, string.Format("Invalid input value. The correct value is {0}", answer));
            }
            return this.PartialView(model);
        }
        

Para atualizar um formulário com outro conteúdo (seja o mesmo formulário, com mensagens de erro, ou um novo formulário) basta retornar uma PartialView, com seu respectivo model.

Para fechar o PopUp, você deve retornar um DialogResult, podendo passar ou não, dados que serão enviados via Json para o client. Para efetuar o processamento destes dados, você pode utilizar a função de callback definida na montagem do link.

>``` javascript
  function funcaoCallback(data){
      console.log('dados retornados pelo servidor' + data);
  }
  
Por fim, caso você precise empilhar um novo PopUp, basta que você configure um ModalDialogActionLink no formulário que será aberto via PopUp. As regras para tratamento de dados são exatamente as mesmas.
