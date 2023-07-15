using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe.Models
{
    internal class TicTacToeModel
    {
        private char[,] _board;
        private char _currentPlayer;
        private int _movesCount;
        private Stack<ICommand> _moveHistory;
        private List<ICommand> _commandHistory;

        public TicTacToeModel()
        {
            DefaultValues();
        }

        private void DefaultValues()
        {
            _board = new char[3, 3];
            _movesCount = 0;
            _currentPlayer = 'X';
            _moveHistory = new Stack<ICommand>();
            _commandHistory = new List<ICommand>(); 
        }

        public void MakeMove(int row, int column, char player)
        {
            if(row <0 || row>2 || column<0 || column > 2 || _board[row, column] != '\0')
            {
                return;
            }

            _board[row, column] = player;

            if (player == 'X')
            {
                _currentPlayer = '0';
            }
            else
            {
                _currentPlayer = 'X';
            }

            _movesCount++;
        }

        public void UndoMove(int  row, int column)
        {
            if(row>=0 && row < 3 && column>=0 && column < 3 && _board[row, column]!='\0')
            {
                _board[row, column] = '\0';

                if (_currentPlayer == 'X')
                {
                    _currentPlayer = '0';
                }
                else
                {
                    _currentPlayer = 'X';
                }

                _movesCount--;
            }
        }

        public void UndoLastMove() 
        { 
            if(_moveHistory.Count > 0)
            {
                ICommand command = _moveHistory.Pop();
                command.Undo();
            }
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();

            _commandHistory.Add(command);

            if(command is MoveCommand)
            {
                _moveHistory.Push(command);
            }
        }

        public bool IsBoardFull()
        {
            if(_movesCount == 9)
            {
                DefaultValues();
                return true;
            }
            return false;
        }

        public char GetPlayerSymbolAt(int row, int column)
        {
            if (_board[row, column] != 'X' && _board[row, column] != '0')
            {
                return _currentPlayer; 
            }
            return _board[row, column];
        }

        public bool HasWon(char player)
        {
            
            for (int row = 0; row < 3; row++)
            {
                if (_board[row, 0] == player && _board[row, 1] == player && _board[row, 2] == player)
                {
                    DefaultValues();
                    return true;
                }
            }


            for (int col = 0; col < 3; col++)
            {
                if (_board[0, col] == player && _board[1, col] == player && _board[2, col] == player)
                {
                    DefaultValues();
                    return true;
                }
            }


            if (_board[0, 0] == player && _board[1, 1] == player && _board[2, 2] == player)
            {
                DefaultValues();
                return true;
            }


            if (_board[0, 2] == player && _board[1, 1] == player && _board[2, 0] == player)
            {
                DefaultValues();
                return true;
            }

            return false;
        }

        public ICommand GetLastMove()
        {
            return _moveHistory.Peek();
        }
    }
}
