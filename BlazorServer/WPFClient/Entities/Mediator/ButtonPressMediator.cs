using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Mediator
{
    public class ButtonPressMediator : IMediator
    {
        private ReadyButtonComponent _buttonComponent;
        private UndoButtonComponent _undoButtonComponent;
        private ElementsHolder _elementsHolder;


        public ButtonPressMediator(ReadyButtonComponent component1, UndoButtonComponent undoButtonComponent, ElementsHolder elementsHolder)
        {
            this._buttonComponent = component1;
            this._buttonComponent.SetMediator(this);
            this._undoButtonComponent = undoButtonComponent;
            this._undoButtonComponent.SetMediator(this);
            this._elementsHolder = elementsHolder;
            this._elementsHolder.SetMediator(this);
        }
        public void Notify(object sender, string ev)
        {
            if (ev == "ButtonComponent")
            {
                Logger logger = Logger.GetInstance();
                Message message = new Message();
                message.SetMessage($"Mediator disables buttons");
                logger.Log(message, 1);
                _elementsHolder.DisableAll();
                _undoButtonComponent.setFlag(true);
            }

            if (ev == "UndoButtonComponent")
            {
                Logger logger = Logger.GetInstance();
                Message message = new Message();
                message.SetMessage($"Mediator enables buttons");
                logger.Log(message, 1);
                _elementsHolder.EnableAll();
            }
        }
    }
}
