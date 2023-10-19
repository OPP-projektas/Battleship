using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Command
{
    public class Invoker
    {
        Logger logger = Logger.GetInstance();
        private ICommand _command;
        private Stack<ICommand> _commandStack = new Stack<ICommand>();

        public void SetCommand(ICommand command)
        {
            this._command = command;
        }
        public void DoCommand()
        {
            if(_command != null)
            {
                _command.Execute();
                _commandStack.Push(_command);
                Message message = new Message();
                message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
                logger.Log(message);
            }
        }
        public void UndoCommand()
        {
            if(_commandStack.Count > 0)
            {
                ICommand lastCommand = _commandStack.Pop();
                lastCommand.Undo();
                Message message = new Message();
                message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
                logger.Log(message);
            }
        }
    }
}
