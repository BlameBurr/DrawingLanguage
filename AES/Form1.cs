namespace AES
{
    public partial class Form1 : Form
    {
        public static Parser parser = new Parser();
        public Form1()
        {
            InitializeComponent();
        }

        private void checkSyntaxBtn_Click(object sender, EventArgs e)
        {
            parser.parseLine(this.cmdTextBox.Text);
        }
    }
}