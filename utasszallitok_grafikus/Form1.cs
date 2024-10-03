using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace utasszallitok_grafikus
{
    public partial class Form1 : Form
    {

        public Form1()
        {
        }

        public void szamitas(object sender, EventArgs e) {
            float a = 0f;
            float b = 0f;
            try {
                a = float.Parse(textBox1.value);
                b = float.Parse(textBox2.value);
            } catch(Exception ex) {
                MessageBox.Show("Nem megfelelő a bemeneti karakterlánc formátuma.");
            }
            var val = Math.sqrt(5*(((a/b)+1)**(2/7)-1));
            listBox1.Items.Add($"qc={a} p0={b} Ma={val}");
            textBox1.value = "";
            textBox2.value = "";

        }
    }
}