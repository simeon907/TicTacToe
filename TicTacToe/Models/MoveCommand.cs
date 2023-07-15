using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    internal class MoveCommand : ICommand
    {
        private TicTacToeModel _model;
        private int _row;
        private int _column;
        private char _player;

        public int Row { get { return _row; } }
        public int Column { get { return _column; } }

        public MoveCommand(TicTacToeModel model, int row, int column, char player)
        {
            _model = model;
            _row = row;
            _column = column;
            _player = player;
        }

        public void Execute()
        {
            _model.MakeMove(_row, _column, _player);
        }

        public void Undo()
        {
            _model.UndoMove(_row, _column);
        }
    }
}
