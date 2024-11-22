﻿using System;
using BancomatClassLibrary;
using System.Windows.Forms;

namespace BankomatForm
{
    public partial class FormBankomatMenu : Form
    {
        private Bank currentBank;
        private Account currentAccount;
        private AutomatedTellerMachine activeBankomat;
        private bool amountEnter = false;
        private int indexOperation = 0;

        public FormBankomatMenu(Bank bank, Account account, AutomatedTellerMachine bankomat)
        {
            InitializeComponent();
            HidePanel();
            panelStart.Visible = true;
            activeBankomat = bankomat;
            currentBank = bank;
            currentAccount = account;
        }

        void HidePanel()
        {
            panelStart.Visible = false;
            panelEnterAmount.Visible = false;
            panelEnterCardNumber.Visible = false;
            panelShowBalance.Visible = false;
            panelPutMoney.Visible = false;
            panelWithDraw.Visible = false;
            labelAmountEnter.Text = "";
            labelCardEnter.Text = "";
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 1;
            }
            else
            {
                labelCardEnter.Text += 1;
            }
        }
        private void btnNum2_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 2;
            }
            else
            {
                labelCardEnter.Text += 2;
            }
        }
        private void btnNum3_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 3;
            }
            else
            {
                labelCardEnter.Text += 3;
            }
        }
        private void btnNum4_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 4;
            }
            else
            {
                labelCardEnter.Text += 4;
            }
        }
        private void btnNum5_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 5;
            }
            else
            {
                labelCardEnter.Text += 5;
            }
        }
        private void btnNum6_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 6;
            }
            else
            {
                labelCardEnter.Text += 6;
            }
        }
        private void btnNum7_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 7;
            }
            else
            {
                labelCardEnter.Text += 7;
            }
        }
        private void btnNum8_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 8;
            }
            else
            {
                labelCardEnter.Text += 8;
            }
        }
        private void btnNum9_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 9;
            }
            else
            {
                labelCardEnter.Text += 9;
            }
        }
        private void btnNum0_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text += 0;
            }
            else
            {
                labelCardEnter.Text += 0;
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            if (amountEnter)
            {
                labelAmountEnter.Text = "";
            }
            else
            {
                labelCardEnter.Text = "";
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            HidePanel();
            panelShowBalance.Visible = true;
            labelShowBalance.Text = currentAccount.CardBalance.ToString("F2") + " грн";
        }

        private void btnPutMoney_Click(object sender, EventArgs e)
        {
            HidePanel();
            panelEnterAmount.Visible = true;
            indexOperation = 1;
            amountEnter = true;
        }

        private void btnWithDraw_Click(object sender, EventArgs e)
        {
            HidePanel();
            panelEnterAmount.Visible = true;
            indexOperation = 2;
            amountEnter = true;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            HidePanel();
            panelEnterCardNumber.Visible = true;
            indexOperation = 3;
            amountEnter = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            double amount;
            switch (indexOperation)
            {
                case 1:
                    if (double.TryParse(labelAmountEnter.Text, out amount))
                    {
                        activeBankomat.PutMoney(currentAccount, amount);
                        panelPutMoney.Visible = true;
                        labelPutMoney.Text = labelAmountEnter.Text + " грн";
                        HidePanel();
                    }
                    else
                    {
                        ShowErrorMessage("Некоректна сума для поповнення.");
                    }
                    indexOperation = 0;
                    break;

                case 2:
                    if (double.TryParse(labelAmountEnter.Text, out amount))
                    {
                        if (activeBankomat.WithDrawMoney(currentAccount, amount))
                        {
                            panelWithDraw.Visible = true;
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Некоректна сума для зняття.");
                    }
                    indexOperation = 0;
                    break;

                case 3:
                    panelEnterCardNumber.Visible = false;
                    panelEnterAmount.Visible = true;
                    amountEnter = true;

                    if (double.TryParse(labelAmountEnter.Text, out amount) && amount > 0)
                    {
                        currentBank.TransferFunds(currentAccount.CardNumber, labelCardEnter.Text, amount);
                        panelStart.Visible = true;
                        indexOperation = 0;
                    }
                    else
                    {
                        ShowErrorMessage("Некоректна сума для переведення.");
                    }
                    break;

                case 0:
                    HidePanel();
                    panelStart.Visible = true;
                    break;
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
