using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Models;

namespace TicTacToe
{
    public partial class MainForm : Form
    {
        private const int BoardSize = 3;
        private Button[,] _cells;
        private TicTacToeModel _model;

        public MainForm()
        {
            InitializeComponent();
            _cells = new Button[BoardSize, BoardSize];
            FillCellsArray();
            _model= new TicTacToeModel();
        }

        private void FillCellsArray()
        {
            _cells[0, 0] = button1;
            _cells[0, 1] = button2;
            _cells[0, 2] = button3;
            _cells[1, 0] = button4;
            _cells[1, 1] = button5;
            _cells[1, 2] = button6;
            _cells[2, 0] = button7;
            _cells[2, 1] = button8;
            _cells[2, 2] = button9;
        }

        private void UpdateCell(int row, int column)
        {
            _cells[row, column].Text = _model.GetPlayerSymbolAt(row, column).ToString();
            _cells[row, column].Enabled = false;
        }

        private void ResetButtons()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _cells[i, j].Text = "";
                    _cells[i, j].Enabled = true;
                }
            }
        }

        private void CheckGameOver()
        {
            if (_model.HasWon('X'))
            {
                MessageBox.Show("Player X has won");
                ResetButtons();
            }
            else if (_model.HasWon('0'))
            {
                MessageBox.Show("Player 0 has won");
                ResetButtons();
            }
            else if (_model.IsBoardFull())
            {
                MessageBox.Show("It is a draw");
                ResetButtons();
            }
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for(int j = 0;j < BoardSize; j++)
                {
                    if (_cells[i, j] == (Button)sender)
                    {
                        int clickedRow = i;
                        int clickedColumn = j;
                        ICommand command = new MoveCommand(_model, clickedRow, clickedColumn, _model.GetPlayerSymbolAt(clickedRow, clickedColumn));
                        _model.ExecuteCommand(command);
                        UpdateCell(clickedRow, clickedColumn);
                        CheckGameOver();
                        break;
                    }
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            ICommand last = _model.GetLastMove();

            int row = (last as MoveCommand).Row;
            int column = (last as MoveCommand).Column;

            _cells[row, column].Text = "";
            _cells[row, column].Enabled = true;

            ICommand command = new UndoCommand(_model);
            _model.ExecuteCommand(command);
        }
    }
}
