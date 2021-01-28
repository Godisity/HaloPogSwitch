﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UI32
{
    public partial class TrainerEnum : UserControl , TrainerUI<byte>
    {
        public int currentIndex = -1;

        ValueStringPair[] vals;
      
        public TrainerEnum(string title, params ValueStringPair[] values)
        {
            InitializeComponent();
            Label_title.Text = title;

           

            vals = new ValueStringPair[values.Length];

          

            for (int i = 0; i < vals.Length; i++)
            {

                vals[i] = values[i];
                
                ListBox.Items.Add(vals[i]);
            }

            
           
        }

        public ValueChanged<byte> onValueChanged { get ; set; }

        public byte GetValue()
        {
            byte val = 0;
            if (vals.Length != 0) 
            {
                
                val = vals[currentIndex].value;
               
            }

            //Console.WriteLine("Byte Val: " + val + " Len " + vals.Length);
            return val;
        }

        public void SetValue(byte value)
        {
            var item = vals.FirstOrDefault(x => x.value == value);
            ListBox.SelectedItem = item;

            

            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i].value == value)
                {
                    currentIndex = i;
                    ListBox.SelectedItem = vals[i];
                    Console.WriteLine(Label_title.Text + " value: " + value +", Button Index: "+ i);
                    return;
                }
            }
           
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentIndex = ListBox.SelectedIndex;

            onValueChanged.Invoke(GetValue());

            //?.Invoke((ListBox.SelectedItem as ValueStringPair).value);
        }


      

    }



    public interface TrainerUI<T>
    {
         T GetValue();
        void SetValue(T value);

         ValueChanged<T> onValueChanged { get; set; }
      
    }
    public class ValueStringPair
    {
        public byte value;
        public string display;

        public ValueStringPair(byte value, string display)
        {
            this.value = value;
            this.display = display;
        }

        public override string ToString()
        {
            return display;
        }

    }
}

public delegate void ValueChanged <T> (T value);