using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace back
{
    partial class Form1 : Form
    {
        public truck truck;
        public DataGridView dataGridView1;

        public Button addButton = new Button();
        public Button refreshButton = new Button();
        public Button button3 = new Button();
        public Button ForwardFromButton = new Button();
        public Button ForwardToButton = new Button();

        private readonly object lockObject1 = new object();
        private readonly object lockObject2 = new object();
        private readonly object lockObject3 = new object();


        public Thread sorting;

        Thread southTo;
        Thread southFrom;
        Thread northTo;
        Thread northFrom;
        Thread westTo;
        Thread westFrom;
        Thread eastTo;
        Thread eastFrom;


        public TextBox adressFrom = new TextBox();
        public TextBox distanceFrom = new TextBox();
        public TextBox regionFrom = new TextBox();
        public TextBox regionTo = new TextBox();
        public TextBox adressTo = new TextBox();
        public TextBox distanceTo = new TextBox();

        public ComboBox comboBox = new ComboBox();
        public ComboBox comboBox1 = new ComboBox();

        static int Counter = 0;


        static int id = 1000;
        string str2 = "Running";
        string str = "Unstarted";
        string str1 = "Stopped";

        public static int count = 0;
        public int indexCurr = 0;
        public int index1 = 0;
        public int index2 = 1;
        public int index3 = 2;
        public int index4 = 3;
        public int index5 = 4;
        public int index6 = 5;
        public int index7 = 6;
        public int index8 = 7;


        public int numVal1;
        public int numVal2;
        public int numVal3;
        public int numVal4;
        public int numVal5;
        public int numVal6;
        public int numVal7;
        public int numVal8;



        hub hub;
        hubSouth hubSouth;
        hubNorth hubNorth;
        hubWest hubWest;
        hubEast hubEast;

        Form1 form;
        package package;

        public Form1()
        {
            InitializeComponent();

        }

        public void create()
        {

            southTo = new Thread(GoToSouth);
            southFrom = new Thread(GoFromSouth);
            northTo = new Thread(GoToNorth);
            northFrom = new Thread(GoFromNorth);
            westTo = new Thread(GoToWest);
            westFrom = new Thread(GoFromWest);
            eastTo = new Thread(GoToEast);
            eastFrom = new Thread(GoFromEast);


            this.form = new Form1();
            dataGridView1 = new DataGridView();
            hubSouth = new hubSouth();
            hubNorth = new hubNorth();
            hubWest = new hubWest();
            hubEast = new hubEast();
            hub = new hub(hubSouth, hubNorth, hubWest, hubEast, form);




            hubSouth = hub.south;
            hubNorth = hub.north;
            hubEast = hub.east;
            hubWest = hub.west;



            addButton = new Button();
            refreshButton = new Button();
            button3 = new Button();



            adressFrom = new TextBox();
            distanceFrom = new TextBox();
            regionFrom = new TextBox();
            regionTo = new TextBox();
            adressTo = new TextBox();
            distanceTo = new TextBox();



            refreshButton.Top = 60;
            refreshButton.Left = 100;
            refreshButton.Text = "refresh";
            refreshButton.Enabled = true;
            refreshButton.Visible = true;
            refreshButton.BackColor = Color.Red;

            ForwardToButton.Width = 100;
            ForwardToButton.Top = 100;
            ForwardToButton.Left = 20;
            ForwardToButton.Text = "forward-to";
            ForwardToButton.BackColor = Color.Red;

            ForwardFromButton.Top = 60;
            ForwardFromButton.Left = 20;
            ForwardFromButton.Text = "forward-from";
            ForwardFromButton.BackColor = Color.Red;


            button3.Enabled = false;
            button3.Text = "-->";
            button3.Left = 300;
            button3.Width = 40;
            button3.Top = 30;

            addButton.Text = "add package / take care of package+add new one" +
                "(same region)";
            addButton.Left = 30;
            addButton.Top = 0;
            addButton.Width = 150;
            addButton.Height = 50;

            addButton.Click += new EventHandler(addButton_Click);
            refreshButton.Click += new EventHandler(buttonStart_Click);
            ForwardFromButton.Click += new EventHandler(ForwardFrom_Click);
            ForwardToButton.Click += new EventHandler(ForwardTo_Click);

            adressFrom.Text = "--adressFrom--";
            distanceFrom.Text = "--distanceFrom--";
            regionFrom.Text = "--region--";


            adressTo.Text = "--adressTo--";
            distanceTo.Text = "--distanceTo--";
            regionTo.Text = "--region--";


            comboBox.Items.Add("south");
            comboBox.Items.Add("north");
            comboBox.Items.Add("west");
            comboBox.Items.Add("east");

            comboBox1.Items.Add("south");
            comboBox1.Items.Add("north");
            comboBox1.Items.Add("west");
            comboBox1.Items.Add("east");

            comboBox.Top = 60;
            comboBox.Left = 200;
            adressFrom.Top = 0;
            adressFrom.Left = 200;
            distanceFrom.Top = 30;
            distanceFrom.Left = 200;

          

            comboBox1.Top = 60;
            comboBox1.Left = 350;
            adressTo.Top = 0;
            adressTo.Left = 350;
            distanceTo.Top = 30;
            distanceTo.Left = 350;

            form.Width = 600;
            form.Height = 500;

            dataGridView1.Left = 30;
            dataGridView1.Top = 200;
            dataGridView1.Width = 550;
            dataGridView1.Height = 250;
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = " truck id";
            dataGridView1.Columns[1].Name = "wait";
            dataGridView1.Columns[2].Name = "in proccess";
            dataGridView1.Columns[3].Name = "remaining time";
            dataGridView1.Columns[4].Name = "status";

            form.Controls.Add(dataGridView1);
            form.Controls.Add(comboBox);
            form.Controls.Add(comboBox1);
            form.Controls.Add(ForwardToButton);
            form.Controls.Add(ForwardFromButton);
            form.Controls.Add(addButton);
            form.Controls.Add(button3);
            form.Controls.Add(refreshButton);
            form.Controls.Add(adressFrom);
            form.Controls.Add(distanceFrom);

            form.Controls.Add(adressTo);
            form.Controls.Add(distanceTo);

            for (int i = 0; i < 8; i++)
            {
                ArrayList row = new ArrayList();

                if (i == 0)
                {
                    row.Add("southFrom");
                }
                if (i == 1)
                {
                    row.Add("northFrom");
                }
                if (i == 2)
                {
                    row.Add("westFrom");
                }
                if (i == 3)
                {
                    row.Add("eastFrom");
                }

                if (i == 4)
                {
                    row.Add("southto");
                }
                if (i == 5)
                {
                    row.Add("northTo");
                }
                if (i == 6)
                {
                    row.Add("westTo");
                }
                if (i == 7)
                {
                    row.Add("eastTo");
                }

                row.Add("");
                row.Add("");
                row.Add(0);
                row.Add("waiting for order");

                dataGridView1.Rows.Add(row.ToArray());

            }
            form.ShowDialog();

        }



        private void Form1_Load(object sender, EventArgs e)
        {


        }


        private void addButton_Click(object sender, EventArgs e)
        {

            id++;
            package = new package(adressFrom.Text, adressTo.Text, Int32.Parse(distanceTo.Text), Int32.Parse(distanceFrom.Text),
            comboBox1.Text, comboBox.Text, id);


            try
            {
                if (package.regionFrom.ToString().Equals("south"))
                {
                    if (hubSouth.packagesFromWait.Count == 0)
                    {


                        if (southFrom.ThreadState.ToString().Equals(str) ||
                            southFrom.ThreadState.ToString().Equals(str1))
                        {

                            hubSouth.addFromTake(package);
                            doWork1(0, package);
                        }
                        else
                        {
                            updateDataGridView5(0);
                            hubSouth.addFromWait(package);

                        }

                    }
                    else
                    {


                        hubSouth.addFromWait(package);

                        for (int i = 1; i < hubSouth.packagesFromWait.Count; i++)
                        {
                            if (hubSouth.packagesFromWait[i].regionFrom.ToString().Equals("south"))
                            {
                                hubSouth.addFromTake(hubSouth.packagesFromWait[i - 1]);
                                hubSouth.packagesFromWait.RemoveAt(i - 1);
                                doWork1(0, hubSouth.packagesFromTake[i - 1]);
                            }

                        }

                    }
                }




                if (package.regionFrom.ToString().Equals("north"))
                {
                    if (hubNorth.packagesFromWait.Count == 0)
                    {

                        if (northFrom.ThreadState.ToString().Equals(str) ||
                            northFrom.ThreadState.ToString().Equals(str1))
                        {

                            hubNorth.addFromTake(package);
                            doWork1(1, package);
                        }
                        else
                        {
                            updateDataGridView5(1);
                            hubNorth.addFromWait(package);

                        }
                    }
                    else
                    {

                        hubNorth.addFromWait(package);


                        for (int i = 1; i < hubNorth.packagesFromWait.Count; i++)
                        {
                            if (hubNorth.packagesFromWait[i].regionFrom.ToString().Equals("north"))
                            {
                                hubNorth.addFromTake(hubNorth.packagesFromWait[i - 1]);
                                hubNorth.packagesFromWait.RemoveAt(i - 1);
                                doWork1(1, hubNorth.packagesFromTake[i - 1]);
                            }
                        }

                    }
                }




                if (package.regionFrom.ToString().Equals("west"))
                {
                    if (hubWest.packagesFromWait.Count == 0)
                    {

                        if (westFrom.ThreadState.ToString().Equals(str)
                            || westFrom.ThreadState.ToString().Equals(str1))
                        {
                            hubWest.addFromTake(package);
                            doWork1(2, package);
                        }
                        else
                        {
                            updateDataGridView5(2);
                            hubWest.addFromWait(package);
                        }
                    }
                    else
                    {

                        hubWest.addFromWait(package);



                        for (int i = 1; i < hubWest.packagesFromWait.Count; i++)
                        {
                            if (hubWest.packagesFromWait[i].regionFrom.ToString().Equals("west"))
                            {
                                hubWest.addFromTake(hubWest.packagesFromWait[i - 1]);
                                hubWest.packagesFromWait.RemoveAt(i - 1);
                                doWork1(2, hubWest.packagesFromTake[i - 1]);

                            }
                        }
                    }
                }



                if (package.regionFrom.ToString().Equals("east"))
                {
                    if (hubEast.packagesFromWait.Count == 0)
                    {


                        if (eastFrom.ThreadState.ToString().Equals(str)
                            || eastFrom.ThreadState.ToString().Equals(str1))
                        {

                            hubEast.addFromTake(package);
                            doWork1(3, package);
                        }
                        else
                        {
                            updateDataGridView5(3);
                            hubEast.addFromWait(package);
                        }
                    }
                    else
                    {

                        hubEast.addFromWait(package);


                        for (int i = 1; i < hubEast.packagesFromWait.Count; i++)
                        {
                            if (hubEast.packagesFromWait[i].regionFrom.ToString().Equals("east"))
                            {
                                hubEast.addFromTake(hubEast.packagesFromTake[i - 1]);
                                hubEast.packagesFromWait.RemoveAt(i - 1);
                                doWork1(3, hubEast.packagesFromTake[i - 1]);

                            }
                        }
                    }




                }
            }



            catch (FormatException f)
            {
                MessageBox.Show("enter a number in distance!!!!");
            }

        }


        public void doWork1(int index, package package)
        {


            if (index == 0 && (southFrom.ThreadState.ToString().Equals(str)
                || southFrom.ThreadState.ToString().Equals(str1)))
            {

                numVal1 += Int32.Parse(distanceFrom.Text) / 10;

                updateDataGridView6(index);



                doWork(index);
            }

            if (index == 1 && (northFrom.ThreadState.ToString().Equals(str) ||
                northFrom.ThreadState.ToString().Equals(str1)))
            {
                numVal2 += Int32.Parse(distanceFrom.Text) / 10;
                updateDataGridView6(index);
                doWork(index);

            }


            if (index == 2 && (westFrom.ThreadState.ToString().Equals(str)
                || westFrom.ThreadState.ToString().Equals(str1)))
            {

                numVal3 += Int32.Parse(distanceFrom.Text) / 10;
                updateDataGridView6(index);
                doWork(index);
            }

            if (index == 3 && (eastFrom.ThreadState.ToString().Equals(str)
                || eastFrom.ThreadState.ToString().Equals(str1)))
            {

                numVal4 += Int32.Parse(distanceFrom.Text) / 10;
                updateDataGridView6(index);
                doWork(index);

            }

        }



        public void doWork(int index)
        {

            string str = "Unstarted";


            if (index == 0 && (southFrom.ThreadState.ToString().Equals(str) ||
                            southFrom.ThreadState.ToString().Equals(str1)))
            {

                if (numVal1 > 10)
                {
                    hubSouth.timeLeft = numVal1;

                    if (southFrom.ThreadState.ToString().Equals(str1))
                    {
                        southFrom = new Thread(GoFromSouth);
                        southFrom.Start();
                    }
                    else
                    {
                        southFrom.Start();

                    }


                }
            }

            if (index == 1 && (northFrom.ThreadState.ToString().Equals(str) ||
                            northFrom.ThreadState.ToString().Equals(str1)))
            {
                if (numVal2 > 10)
                {
                    hubNorth.timeLeft = numVal2;

                    if (northFrom.ThreadState.ToString().Equals(str1))
                    {
                        northFrom = new Thread(GoFromNorth);
                        northFrom.Start();

                    }
                    else
                    {
                        northFrom.Start();

                    }


                }
            }

            if (index == 2 && (westFrom.ThreadState.ToString().Equals(str) ||
                            westFrom.ThreadState.ToString().Equals(str1)))
            {


                if (numVal3 > 10)
                {
                    hubWest.timeLeft = numVal3;

                    if (westFrom.ThreadState.ToString().Equals(str1))
                    {

                        westFrom = new Thread(GoFromWest);
                        westFrom.Start();
                    }
                    else
                    {
                        westFrom.Start();
                    }

                }
            }

            if (index == 3 && (eastFrom.ThreadState.ToString().Equals(str)
                || eastFrom.ThreadState.ToString().Equals(str1)))
            {

                if (numVal4 > 10)
                {
                    hubEast.timeLeft = numVal3;

                    if (eastFrom.ThreadState.ToString().Equals(str1))
                    {
                        eastFrom = new Thread(GoFromEast);
                        eastFrom.Start();
                    }
                    else
                    {
                        eastFrom.Start();

                    }

                }

            }
        }





        public void GoFromEast()
        {
            updateDataGridView1(3);
            ForwardFromButton.BackColor = Color.Red;
            ForwardToButton.BackColor = Color.Red;
            refreshButton.BackColor = Color.Red;


            while (numVal4 > 0)
            {
                Thread.Sleep(500);
                numVal4--;
                updateDataGridViewTime(3, numVal4);

            }

            updateDataGridViewInit(3);

            for (int i = 0; i < hubEast.packagesFromTake.Count; i++)
            {
                hub.sort(hubEast.packagesFromTake[i]);

            }

            hubSouth.packagesToWait = hub.south.packagesToWait;
            hubWest.packagesToWait = hub.west.packagesToWait;
            hubNorth.packagesToWait = hub.north.packagesToWait;
            hubEast.packagesToWait = hub.east.packagesToWait;


            if (southTo.ThreadState.ToString().Equals(str)
               || southTo.ThreadState.ToString().Equals(str1))
            {
                sumSouth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if (northTo.ThreadState.ToString().Equals(str)
                || northTo.ThreadState.ToString().Equals(str1))
            {
                sumNorth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if (westTo.ThreadState.ToString().Equals(str)
              || westTo.ThreadState.ToString().Equals(str1))
            {
                sumWest();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }

            if (eastTo.ThreadState.ToString().Equals(str)
              || eastTo.ThreadState.ToString().Equals(str1))
            {
                sumEast();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }





        }

        public void GoFromNorth()
        {
            ForwardFromButton.BackColor = Color.Red;
            ForwardToButton.BackColor = Color.Red;
            refreshButton.BackColor = Color.Red;


            updateDataGridView1(1);

            while (numVal2 > 0)
            {
                Thread.Sleep(500);
                numVal2--;
                updateDataGridViewTime(1, numVal2);

            }


            updateDataGridViewInit(1);

            for (int i = 0; i < hubNorth.packagesFromTake.Count; i++)
            {
                hub.sort(hubNorth.packagesFromTake[i]);


            }
            hubSouth.packagesToWait = hub.south.packagesToWait;
            hubWest.packagesToWait = hub.west.packagesToWait;
            hubNorth.packagesToWait = hub.north.packagesToWait;
            hubEast.packagesToWait = hub.east.packagesToWait;

            if (southTo.ThreadState.ToString().Equals(str)
                || southTo.ThreadState.ToString().Equals(str1))
            {
                sumSouth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if (northTo.ThreadState.ToString().Equals(str)
                || northTo.ThreadState.ToString().Equals(str1))
            {
                sumNorth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if (westTo.ThreadState.ToString().Equals(str)
              || westTo.ThreadState.ToString().Equals(str1))
            {
                sumWest();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }

            if (eastTo.ThreadState.ToString().Equals(str)
              || eastTo.ThreadState.ToString().Equals(str1))
            {
                sumEast();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }




        }



        public void GoFromWest()
        {
            ForwardFromButton.BackColor = Color.Red;
            ForwardToButton.BackColor = Color.Red;
            refreshButton.BackColor = Color.Red;


            updateDataGridView1(2);

            while (numVal3 > 0)
            {
                Thread.Sleep(500);
                numVal3--;
                updateDataGridViewTime(2, numVal3);


            }


            updateDataGridViewInit(2);


            for (int i = 0; i < hubWest.packagesFromTake.Count; i++)
            {
                hub.sort(hubWest.packagesFromTake[i]);


            }
            hubSouth.packagesToWait = hub.south.packagesToWait;
            hubWest.packagesToWait = hub.west.packagesToWait;
            hubNorth.packagesToWait = hub.north.packagesToWait;
            hubEast.packagesToWait = hub.east.packagesToWait;

            if (southTo.ThreadState.ToString().Equals(str)
               || southTo.ThreadState.ToString().Equals(str1))
            {
                sumSouth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if (northTo.ThreadState.ToString().Equals(str)
                || northTo.ThreadState.ToString().Equals(str1))
            {
                sumNorth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if (westTo.ThreadState.ToString().Equals(str)
              || westTo.ThreadState.ToString().Equals(str1))
            {
                sumWest();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }

            if (eastTo.ThreadState.ToString().Equals(str)
              || eastTo.ThreadState.ToString().Equals(str1))
            {
                sumEast();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }



        }


        public void GoFromSouth()
        {
            ForwardFromButton.BackColor = Color.Red;
            ForwardToButton.BackColor = Color.Red;
            refreshButton.BackColor = Color.Red;

            updateDataGridView1(0);

            while (numVal1 > 0)
            {
                Thread.Sleep(500);
                numVal1--;
                updateDataGridViewTime(0, numVal1);
            }


            updateDataGridViewInit(0);

            for (int i = 0; i < hubSouth.packagesFromTake.Count; i++)
            {
                hub.sort(hubSouth.packagesFromTake[i]);
            }

            hubSouth.packagesToWait = hub.south.packagesToWait;
            hubWest.packagesToWait = hub.west.packagesToWait;
            hubNorth.packagesToWait = hub.north.packagesToWait;
            hubEast.packagesToWait = hub.east.packagesToWait;


            if ((southTo.ThreadState.ToString().Equals(str)
               || southTo.ThreadState.ToString().Equals(str1)) && hubSouth.packagesToWait.Count != 0)
            {
                sumSouth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if ((northTo.ThreadState.ToString().Equals(str)
                || northTo.ThreadState.ToString().Equals(str1)) && hubNorth.packagesToWait.Count != 0)
            {
                sumNorth();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }
            if ((westTo.ThreadState.ToString().Equals(str)
              || westTo.ThreadState.ToString().Equals(str1)) && hubWest.packagesToWait.Count != 0)
            {
                sumWest();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }

            if ((eastTo.ThreadState.ToString().Equals(str)
              || eastTo.ThreadState.ToString().Equals(str1)) && hubEast.packagesToWait.Count != 0)
            {
                sumEast();
            }
            else
            {
                refreshButton.BackColor = Color.Green;
            }






        }



        public void sumSouth()
        {

            for (int i = 0; i < hubSouth.packagesToWait.Count; i++)
            {

                while (numVal5 < 10)
                {
                    if (hubSouth.packagesToWait[0].regionTo.ToString().Equals("south"))
                    {

                        hubSouth.addtoTake(hubSouth.packagesToWait[0]);
                        numVal5 += hubSouth.packagesToWait[0].distanceTo / 10;
                        updateDataGridView3(4, hubSouth.packagesToWait[0], numVal5);
                        hubSouth.packagesToWait.RemoveAt(0);
                    }

                }



            }


            refreshButton.BackColor = Color.Green;
        }



        public void sumNorth()
        {

            for (int i = 0; i < hubNorth.packagesToWait.Count; i++)
            {

                while (numVal6 < 10)
                {
                    if (hubNorth.packagesToWait[0].regionTo.ToString().Equals("north"))
                    {
                        hubNorth.addtoTake(hubNorth.packagesToWait[0]);
                        numVal6 += hubNorth.packagesToWait[0].distanceTo / 10;
                        updateDataGridView3(5, hubNorth.packagesToWait[0], numVal6);
                        hubNorth.packagesToWait.RemoveAt(0);
                    }
                }


            }

            refreshButton.BackColor = Color.Green;
        }


        public void sumWest()
        {

            for (int i = 0; i < hubWest.packagesToWait.Count; i++)
            {

                while (numVal7 < 10)

                    if (hubWest.packagesToWait[0].regionTo.ToString().Equals("west"))
                    {
                        hubWest.addtoTake(hubWest.packagesToWait[0]);
                        numVal7 += hubWest.packagesToWait[0].distanceTo / 10;
                        updateDataGridView3(6, hubWest.packagesToWait[0], numVal7);
                        hubWest.packagesToWait.RemoveAt(0);

                    }


            }

            refreshButton.BackColor = Color.Green;
        }


        public void sumEast()
        {

            for (int i = 0; i < hubEast.packagesToWait.Count; i++)
            {

                while (numVal8 < 10)

                    if (hubEast.packagesToWait[0].regionTo.ToString().Equals("east"))
                    {

                        hubEast.addtoTake(hubEast.packagesToWait[0]);
                        numVal8 += hubEast.packagesToWait[0].distanceTo / 10;
                        updateDataGridView3(7, hubEast.packagesToWait[0], numVal8);
                        hubEast.packagesToWait.RemoveAt(0);


                    }

            }

            refreshButton.BackColor = Color.Green;
        }

        private void ForwardFrom_Click(object sender, EventArgs e)
        {


            if ((southFrom.ThreadState.ToString().Equals(str)
                || southFrom.ThreadState.ToString().Equals(str1)) && hubSouth.packagesFromTake.Count != 0)
            {

                southFrom = new Thread(GoFromSouth);
                southFrom.Start();

            }
            if ((northFrom.ThreadState.ToString().Equals(str)
                || northFrom.ThreadState.ToString().Equals(str1)) && hubNorth.packagesFromTake.Count != 0)
            {

                northFrom = new Thread(GoFromNorth);
                northFrom.Start();

            }
            if ((westFrom.ThreadState.ToString().Equals(str)
               || westFrom.ThreadState.ToString().Equals(str1)) && hubWest.packagesFromTake.Count != 0)
            {

                westFrom = new Thread(GoFromWest);
                westFrom.Start();

            }
            if ((eastFrom.ThreadState.ToString().Equals(str)
              || eastFrom.ThreadState.ToString().Equals(str1)) && hubEast.packagesFromTake.Count != 0)
            {

                eastFrom = new Thread(GoFromEast);
                eastFrom.Start();

            }
        }


        private void ForwardTo_Click(object sender, EventArgs e)
        {

            if ((southTo.ThreadState.ToString().Equals(str)
                || southTo.ThreadState.ToString().Equals(str1)) && hubSouth.packagesToTake.Count != 0)
            {
                updateDataGridView1(4);
                southTo = new Thread(GoToSouth);
                southTo.Start();

            }
            if ((northTo.ThreadState.ToString().Equals(str)
                || northTo.ThreadState.ToString().Equals(str1)) && hubNorth.packagesToTake.Count != 0)
            {
                updateDataGridView1(5);
                northTo = new Thread(GoToNorth);
                northTo.Start();

            }
            if ((westTo.ThreadState.ToString().Equals(str)
               || westTo.ThreadState.ToString().Equals(str1)) && hubWest.packagesToTake.Count != 0)
            {
                updateDataGridView1(6);
                westTo = new Thread(GoToWest);
                westTo.Start();

            }
            if ((eastTo.ThreadState.ToString().Equals(str)
              || eastTo.ThreadState.ToString().Equals(str1)) && hubWest.packagesToTake.Count != 0)
            {
                updateDataGridView1(7);
                eastTo = new Thread(GoToEast);
                eastTo.Start();

            }




        }






        private void buttonStart_Click(object sender, EventArgs e)
        {
            Thread.Sleep(500);

            refreshButton.BackColor = Color.Red;


            if (numVal5 > 10 && (southTo.ThreadState.ToString().Equals(str)
            || southTo.ThreadState.ToString().Equals(str1)))
            {
                if (southTo.ThreadState.ToString().Equals(str1))
                {
                    hubSouth.packagesToTake = hubSouth.packagesToWait;
                    hubSouth.packagesToWait.Clear();
                    updateDataGridView1(4);
                    southTo = new Thread(GoToSouth);
                    southTo.Start();
                }
                else
                {
                    hubSouth.packagesToTake = hubSouth.packagesToWait;
                    hubSouth.packagesToWait.Clear();
                    updateDataGridView1(4);
                    southTo.Start();

                }
            }
            else
            {
                for (int i = 0; i < hubSouth.packagesToWait.Count; i++)
                {
                    updateDataGridView4(4, hubSouth.packagesToWait[i]);

                }


            }




            if (numVal6 > 10 && (northTo.ThreadState.ToString().Equals(str)
                || northTo.ThreadState.ToString().Equals(str1)))
            {
                if (northTo.ThreadState.ToString().Equals(str1))
                {
                    hubNorth.packagesToTake = hubNorth.packagesToWait;
                    hubNorth.packagesToWait.Clear();
                    updateDataGridView1(5);
                    northTo = new Thread(GoToNorth);
                    northTo.Start();

                }
                else
                {
                    hubNorth.packagesToTake = hubNorth.packagesToWait;
                    hubNorth.packagesToWait.Clear();
                    updateDataGridView1(5);
                    northTo.Start();
                }

            }
            else
            {

                for (int i = 0; i < hubNorth.packagesToWait.Count; i++)
                {
                    updateDataGridView4(5, hubNorth.packagesToWait[0]);
                }



            }


            if (numVal7 > 10 && (westTo.ThreadState.ToString().Equals(str) ||
                westTo.ThreadState.ToString().Equals(str1)))
            {
                if (westTo.ThreadState.ToString().Equals(str1))
                {
                    hubWest.packagesToTake = hubWest.packagesToWait;
                    hubWest.packagesToWait.Clear();
                    updateDataGridView1(6);
                    westTo = new Thread(GoToWest);
                    westTo.Start();

                }
                else
                {
                    hubWest.packagesToTake = hubWest.packagesToWait;
                    hubWest.packagesToWait.Clear();
                    updateDataGridView1(6);
                    westTo.Start();


                }

            }
            else
            {
                for (int i = 0; i < hubWest.packagesToWait.Count; i++)
                {
                    updateDataGridView4(6, hubWest.packagesToWait[i]);
                }


            }



            if (numVal8 > 10 && (eastTo.ThreadState.ToString().Equals(str)
                || eastTo.ThreadState.ToString().Equals(str1)))
            {
                if (eastTo.ThreadState.ToString().Equals(str1))
                {
                    hubEast.packagesToTake = hubEast.packagesToWait;
                    hubEast.packagesToWait.Clear();
                    updateDataGridView1(7);
                    eastTo = new Thread(GoToEast);
                    eastTo.Start();
                }
                else
                {
                    hubEast.packagesToTake = hubEast.packagesToWait;
                    hubEast.packagesToWait.Clear();
                    updateDataGridView1(7);
                    eastTo.Start();
                }

            }
            else
            {
                for (int i = 0; i < hubEast.packagesToWait.Count; i++)
                {
                    updateDataGridView4(7, hubEast.packagesToWait[i]);
                }


            }


        }



        public void GoToNorth()
        {

            ForwardToButton.BackColor = Color.Red;

            while (numVal6 > 0)
            {

                Thread.Sleep(500);
                numVal6--;
                updateDataGridViewTime(5, numVal6);

            }


            updateDataGridViewInit(5);
            checkwWait();
            hubNorth.packagesToTake.Clear();
            northTo.Interrupt();


        }

        public void GoToWest()
        {

            ForwardToButton.BackColor = Color.Red;
            while (numVal7 > 0)
            {
                Thread.Sleep(500);
                numVal7--;
                updateDataGridViewTime(6, numVal7);


            }

            updateDataGridViewInit(6);
            checkwWait();
            hubWest.packagesToTake.Clear();
            westTo.Interrupt();


        }

        public void GoToSouth()
        {


            ForwardToButton.BackColor = Color.Red;
            while (numVal5 > 0)
            {
                Thread.Sleep(500);
                numVal5--;
                updateDataGridViewTime(4, numVal5);


            }

            updateDataGridViewInit(4);
            checkwWait();
            southTo.Interrupt();


        }


        public void GoToEast()
        {
            ForwardToButton.BackColor = Color.Red;

            while (numVal8 > 0)
            {
                Thread.Sleep(500);
                numVal8--;
                updateDataGridViewTime(7, numVal8);

            }
            updateDataGridViewInit(7);
            checkwWait();
            eastTo.Interrupt();




        }

        public void checkwWait()
        {

            if (hubEast.packagesFromWait.Count != 0 || hubNorth.packagesFromWait.Count != 0
           || hubSouth.packagesFromWait.Count != 0
           || hubWest.packagesFromWait.Count != 0)
            {

                for (int i = 0; i < hubWest.packagesFromWait.Count +
                   hubNorth.packagesFromWait.Count + hubSouth.packagesFromWait.Count +
                   hubWest.packagesFromWait.Count; i++)
                {

                    if (hubSouth.packagesFromWait.Count != 0)
                    {
                        numVal1 += hubSouth.packagesFromWait[i].distanceTo / 10;
                        updateDataGridView1(0);
                        hubSouth.packagesFromWait.RemoveAt(i);


                    }
                    if (hubNorth.packagesFromWait.Count != 0)
                    {

                        numVal2 += hubNorth.packagesFromWait[i].distanceTo / 10;
                        updateDataGridView1(1);
                        hubNorth.packagesFromWait.RemoveAt(i);

                    }
                    if (hubWest.packagesFromWait.Count != 0)
                    {

                        numVal3 += hubWest.packagesFromWait[i].distanceTo / 10;
                        updateDataGridView1(2);
                        hubWest.packagesFromWait.RemoveAt(i);
                    }
                    if (hubEast.packagesFromWait.Count != 0)
                    {
                        numVal4 += hubEast.packagesFromWait[i].distanceTo / 10;
                        updateDataGridView1(3);
                        hubEast.packagesFromWait.RemoveAt(i);
                    }


                }
                ForwardFromButton.BackColor = Color.Green;

            }


            if (hubEast.packagesToWait.Count != 0 || hubNorth.packagesToWait.Count != 0
          || hubSouth.packagesToWait.Count != 0
          || hubWest.packagesToWait.Count != 0)
            {

                for (int i = 0; i < hubWest.packagesToWait.Count +
                   hubNorth.packagesToWait.Count + hubSouth.packagesToWait.Count +
                   hubWest.packagesToWait.Count; i++)
                {

                    if (hubSouth.packagesToWait.Count != 0)
                    {
                        numVal5 += hubSouth.packagesToWait[i].distanceTo / 10;
                        updateDataGridView1(4);
                        hubSouth.packagesToWait.RemoveAt(i);


                    }
                    if (hubNorth.packagesToWait.Count != 0)
                    {

                        numVal6 += hubNorth.packagesToWait[i].distanceTo / 10;
                        updateDataGridView1(5);
                        hubNorth.packagesToWait.RemoveAt(i);

                    }
                    if (hubWest.packagesToWait.Count != 0)
                    {

                        numVal7 += hubWest.packagesToWait[i].distanceTo / 10;
                        updateDataGridView1(6);
                        hubWest.packagesToWait.RemoveAt(i);
                    }
                    if (hubEast.packagesToWait.Count != 0)
                    {
                        numVal8 += hubEast.packagesToWait[i].distanceTo / 10;
                        updateDataGridView1(7);
                        hubEast.packagesToWait.RemoveAt(i);
                    }


                }
                ForwardToButton.BackColor = Color.Green;

            }
        }


        public void updateDataGridView1(int index)
        {
            if (this.dataGridView1.Rows[index].Cells[1].Value.ToString().Equals(""))
            {

                this.dataGridView1.Rows[index].Cells[2].Value = this.dataGridView1.Rows[index].Cells[2].Value;
                this.dataGridView1.Rows[index].Cells[1].Value = "";

            }
            else
            {
                this.dataGridView1.Rows[index].Cells[2].Value = this.dataGridView1.Rows[index].Cells[1].Value;
                this.dataGridView1.Rows[index].Cells[1].Value = "";
            }

        }



        public void updateDataGridView2(int index, List<package> p)
        {

            for (int i = 0; i < p.Count; i++)
            {
                this.dataGridView1.Rows[index].Cells[2].Value += p[i].id + " " + p[i].adressTo;
                this.dataGridView1.Rows[index].Cells[1].Value = "";
            }

        }
        public void updateDataGridView3(int index, package p, int distance)
        {

            this.dataGridView1.Rows[index].Cells[1].Value = p.regionTo + " " + p.id;
            this.dataGridView1.Rows[index].Cells[3].Value = distance;


        }

        public void updateDataGridView4(int index, package p)
        {
            this.dataGridView1.Rows[index].Cells[1].Value = p.regionTo + " " + p.id;
        }

        public void updateDataGridView5(int index)
        {
            this.dataGridView1.Rows[index].Cells[1].Value += "," + package.adressFrom + package.id;

        }
        public void updateDataGridView6(int index)
        {

            this.dataGridView1.Rows[index].Cells[1].Value += "," + package.adressFrom + package.id;
            this.dataGridView1.Rows[index].Cells[2].Value = "";
            this.dataGridView1.Rows[index].Cells[3].Value = numVal1;
            this.dataGridView1.Rows[index].Cells[4].Value = "take from customer";


        }


        public void updateDataGridViewTime(int index, int time)
        {

            this.dataGridView1.Rows[index].Cells[3].Value = time;
            this.dataGridView1.Rows[index].Cells[4].Value = "take from customer";
        }
        public void updateDataGridViewInit(int index)
        {

            this.dataGridView1.Rows[index].Cells[2].Value = "";
            this.dataGridView1.Rows[index].Cells[4].Value = "wait for order";
        }
    }
}

