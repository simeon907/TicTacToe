using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    internal class UndoCommand : ICommand
    {
        private TicTacToeModel _model;

        public UndoCommand(TicTacToeModel model)
        {
            _model = model;
        }

        public void Execute()
        {
            _model.UndoLastMove();
        }

        public void Undo()
        {
            
        }
    }
}
