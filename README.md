# About

BotFramework makes is easy to build robust and fast bots in the shortest possible time. The framework is fully extensible and so you can customize the behaviour to meet your needs.


# Installation

The first thing you need is an [ASP.Net](https://docs.microsoft.com/en-us/aspnet/core) project. You can use an existing project or create a new one. 

Your project should be configured to use GitHub packages since the that is where the framework is hosted. If you don't already have that setup you can follow the steps below.


## 1. Setup GitHub Packages

1. Authenticate to GitHub Packages. For more information, see "[Authenticating to GitHub Packages.](https://help.github.com/en/packages/using-github-packages-with-your-projects-ecosystem/configuring-dotnet-cli-for-use-with-github-packages#authenticating-to-github-packages)".

2. Add `ItemGroup` and configure the `PackageReference` field in the `.csproj` file for your project, replacing the version number with the version you want to use. It is always best to use the [latest version](https://github.com/jowusu837/BotFramework/packages). 
```c#
<Project Sdk="Microsoft.NET.Sdk">
	...
	<ItemGroup>
      <PackageReference Include="BotFramework" Version="1.1.0" />
    </ItemGroup>
    ...
</Project>
```

3. Install the package with the `dotnet restore` command. 


## 2. Configure your bot

You need to modify your project's `Startup.cs` file like so:
```c#
using  Framework;

public void ConfigureServices(IServiceCollection services)
{
	...
	services.AddBot(builder =>
	{
		builder.UseInMemorySessionDriver();
		builder.SetEntryPointConversation<WelcomeConversation>();
	});
	services.AddControllers();
}
```

Two things to note in the above snippet is that our bot is using an in-memory session driver which comes with the framework. We also set `WelcomeConversation` as our entrypoint conversation. We'll talk more about conversations later but this is a very vital step. 


## 3. Setup webhook

You need to setup a dedicated controller action that will be used to handle webhook responses for your bot. Here's an example:
```c#
using System.Text.Json;
using System.Threading.Tasks;
using Framework;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WebhookController : ControllerBase
	{
		private readonly IWebhookHandler _handler;

		public WebhookController(IWebhookHandler handler)
		{
			_handler = handler;
		}

		[HttpPost]
		[Route("{driverName}")]
		public async Task<JsonResult> Post(string driverName)
		{
			var response = await _handler.HandleRequest(driverName);
			return new JsonResult(response, new JsonSerializerOptions
			{
				PropertyNamingPolicy = null
			});
		}
	}

}
```


# Conversations

Conversations provide the means by which your bot receives and sends messages. This is where all the interactions are housed.

A conversation is a simply a class that extends `Conversation` abstract class. Here's an example conversation:
```c#
using System.Threading.Tasks;
using Framework;
using Framework.Conversations;

namespace Demo.Conversations
{
	public class CheckSystemStatusConversation : Conversation
	{
		public CheckSystemStatusConversation(IBot bot) : base(bot)
		{
		}

		public override async Task Run()
		{
			await Bot.Reply("All systems functioning at 100%!");
		}
	}
}
```

A conversation must implement the `Run()` method which is where the conversation starts.

Each conversation has access to the bot instance via `Bot` property. You will use this to interact with your bot from your conversation class.


## Sending messages

A conversation generally houses a bunch of questions and answers. If you want to send a message i.e. you want your bot to ask the user a question or tell the user something.

### Asking questions

You can ask a question like so:
```c#
Bot.Ask(question, answer => {})
```
The `question` parameter can be a simple string like `What is your name?` or a question object like:
```c#
var question = new Question
{
	Text = "Hi there! How may I help you?",
	Options = new List<Option>
	{
		new Option
		{
			Text = "Check system status",
			Next = answer => new CheckSystemStatusConversation(Bot).Run()
		},
		new Option
		{
			Text = "Set system date and time",
			Next = answer => SetDateTime()
		},
		new Option
		{
			Text = "Logoff",
			Next = answer => Logoff()
		}
	}
};
```


### Replying to messages

You can reply to messages like so:
```c#
Bot.Reply("Hope to see you again soon!");
```
And you can do this in the next function (which is the second parameter) of `Bot.Ask()`
```c#
Bot.Ask("What date is today?", answer  =>  Bot.Reply($"System date updated to {answer.Value}"));
```
or via `Option` question property:
```c#
new Option
{
	Text = "Check system status",
	Next = answer => Bot.Reply($"System date updated to {answer.Value}"))
},
```


## Entrypoint conversation

You need to mark one of your conversations as an entrypoint conversation. This is the first conversation that your bot will execute. 

To set a conversation as an entrypoint conversation, the conversation class much also implement `IEntrypointConversation` interface. Secondly, you must let your bot know what entrypoint conversation to use by specifying it in your `Startup.cs` file as mentioned above.


# Session handling

TBD;
