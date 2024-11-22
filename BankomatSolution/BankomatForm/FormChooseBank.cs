﻿using System;
using BancomatClassLibrary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BankomatForm
{
    public partial class FormChooseBank : Form
    {
        static Bank[] banks = {
            new Bank("Приват банк"),
            new Bank("Моно банк"),
        };

        static AutomatedTellerMachine activeBankomat;
        static Account currentAccount;
        public FormChooseBank()
        {
            InitializeComponent();
            for (int i = 0; i < banks.Length; i++)
            {
                banks[i].Message += MessageHandler;
            }
            this.Load += new EventHandler(FormChooseBank_Load);
        }

        static void MessageHandler(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAddress.Items.Clear();
            foreach (var bankomat in banks[cbBank.SelectedIndex].AtmList)
            {
                cbAddress.Items.Add(bankomat.BankomatAddress);
            }
        }
        private void FormChooseBank_Load(object sender, EventArgs e)
        {
            banks[0].AddAtm(1, "Велика Бердичiвська 52", 20000);
            banks[0].AddAtm(1, "Довженка 43", 50000);
            banks[1].AddAtm(2, "Київська 57", 1000);
            banks[1].AddAtm(2, "Михайлівська 57", 20000);

            foreach (var bank in banks)
            {
                cbBank.Items.Add(bank.BankName);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (cbAddress.SelectedIndex == -1)
            {
                MessageBox.Show("Банкомат не обрано", "Помилка");
            }
            else
            {
                activeBankomat = banks[cbBank.SelectedIndex].AtmList.Find(a => a.BankomatAddress == cbAddress.SelectedItem.ToString());
                labelAddress.Text = "вул." + cbAddress.SelectedItem.ToString();
                panelChooseTerminal.Visible = false;
                panelEnterAccount.Visible = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            panelChooseTerminal.Visible = true;
            panelEnterAccount.Visible = false;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormRegistration createAccountForm = new FormRegistration(banks[cbBank.SelectedIndex]);

            createAccountForm.ShowDialog();

            this.Show();

        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAuthorization enterAccountForm = new FormAuthorization(banks[cbBank.SelectedIndex], activeBankomat);

            enterAccountForm.ShowDialog();
            this.Show();
        }
    }
}
