using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionBuffer
{

    public interface IAction
    {
        bool DoAction();
    }

    public class BufferAction
    {
        private readonly int _timers;
        private readonly int _counter;
        private readonly IAction _action;

        private int _curentTimer;
        private int _currentCounter;

        public bool ActionStart;
        public bool ActionDone;

        public BufferAction(int timers, int counter, IAction action)
        {
            _timers = timers;
            _counter = counter;
            _action = action;
        }

        public void RecieveResult(bool result)
        {
            if (ActionStart)
            {
                return;
            }

            if (result)
            {
                _curentTimer = 0;
                return;
            }

            _curentTimer += 1;
            if (_curentTimer >= _timers)
            {
                _currentCounter += 1;
                _curentTimer = 0;
            }

            if (_currentCounter >= _counter)
            {
                ActionStart = true;
                ActionDone= _action.DoAction();
            }

        }
    }
}
