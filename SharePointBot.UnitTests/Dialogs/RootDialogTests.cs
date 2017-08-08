using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePointBot.UnitTests.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace SharePointBot.UnitTests.Dialogs
{
    [TestClass]
    public class RootDialogTests : DialogTestBase
    {
        [TestMethod]
        public async Task ShouldReturnEcho()
        {
            // Instantiate dialog to test
            IDialog<object> rootDialog = new RootDialog();

            // Create in-memory bot environment
            Func<IDialog<object>> MakeRoot = () => rootDialog;
            using (new FiberTestBase.ResolveMoqAssembly(rootDialog))
            {
                using (var container = Build(Options.MockConnectorFactory | Options.ScopedQueue, rootDialog))
                {
                    // Create a message to send to bot
                    var toBot = DialogTestBase.MakeTestMessage();
                    toBot.From.Id = Guid.NewGuid().ToString();
                    toBot.Text = "login";

                    // Send message and check the answer.
                    IMessageActivity toUser = await GetResponse(container, MakeRoot, toBot);

                    // Verify the result
                    // await Conversation.SendAsync(toBot, MakeRoot, CancellationToken.None);
                }
            }
        }
    }
}
