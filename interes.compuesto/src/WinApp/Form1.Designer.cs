namespace WinApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Form1";

        //new label Capital Inicial
        var label1 = new System.Windows.Forms.Label();
        label1.Text = "Capital Inicial";
        label1.Location = new System.Drawing.Point(10, 10);
        this.Controls.Add(label1);        
        var text1 = new System.Windows.Forms.TextBox();
        text1.Text = "1000";
        text1.Location = new System.Drawing.Point(10, 30);
        this.Controls.Add(text1); //1

        //new label Aportacion Mensual
        var label2 = new System.Windows.Forms.Label();
        label2.Text = "Aportacion Mensual";
        label2.Location = new System.Drawing.Point(10, 60);
        this.Controls.Add(label2);
        var text2 = new System.Windows.Forms.TextBox();
        text2.Text = "100";
        text2.Location = new System.Drawing.Point(10, 80);
        this.Controls.Add(text2); //3

        //new label Tasa de Interes Anual
        var label3 = new System.Windows.Forms.Label();
        label3.Text = "Tasa de Interes Anual";
        label3.Location = new System.Drawing.Point(10, 110);
        this.Controls.Add(label3);
        var text3 = new System.Windows.Forms.TextBox();
        text3.Text = "5";
        text3.Location = new System.Drawing.Point(10, 130);
        this.Controls.Add(text3); //5

        //new label Plazo en Anios
        var label4 = new System.Windows.Forms.Label();
        label4.Text = "Plazo en Años";
        label4.Location = new System.Drawing.Point(10, 160);
        this.Controls.Add(label4);
        var text4 = new System.Windows.Forms.TextBox();
        text4.Text = "10";
        text4.Location = new System.Drawing.Point(10, 180);
        this.Controls.Add(text4); //7

        //new label Numero de Veces Capitalizacion
        var label5 = new System.Windows.Forms.Label();
        label5.Text = "Numero de Veces Capitalizacion";
        label5.Location = new System.Drawing.Point(10, 210);
        this.Controls.Add(label5);
        var text5 = new System.Windows.Forms.TextBox();
        text5.Text = "12";
        text5.Location = new System.Drawing.Point(10, 230);
        this.Controls.Add(text5); //9
        
        //new button Calcular
        var button1 = new System.Windows.Forms.Button();
        button1.Text = "Calcular";
        button1.Location = new System.Drawing.Point(10, 260);
        button1.Click += new System.EventHandler(this.button1_Click);
        this.Controls.Add(button1);

        //new label Rendimiento
        var label6 = new System.Windows.Forms.Label();
        label6.Text = "Rendimiento";
        label6.Location = new System.Drawing.Point(10, 290);
        this.Controls.Add(label6);

        //new label Inversion
        var label7 = new System.Windows.Forms.Label();
        label7.Text = "Inversion";
        label7.Location = new System.Drawing.Point(10, 320);
        this.Controls.Add(label7);
        
    }
    #endregion

    //button1_Click
    private void button1_Click(object sender, System.EventArgs e)
    {
        var capitalInicial = double.Parse(this.Controls[1].Text);
        var aportacionMensual = double.Parse(this.Controls[3].Text);
        var tasaInteresAnual = double.Parse(this.Controls[5].Text);
        var plazoEnAnios = int.Parse(this.Controls[7].Text);
        var numeroDeVecesCapitalizacion = int.Parse(this.Controls[9].Text);
        var interesCompuesto = new Algoritmos.InteresCompuesto();
        var result = interesCompuesto.Calcular(capitalInicial, aportacionMensual, tasaInteresAnual, numeroDeVecesCapitalizacion, plazoEnAnios);
               
        this.Controls[11].Text = result.ToString("#0.00");
        this.Controls[12].Text = (capitalInicial + aportacionMensual * plazoEnAnios * numeroDeVecesCapitalizacion).ToString("#0.00");
        
    }
    
    
}
