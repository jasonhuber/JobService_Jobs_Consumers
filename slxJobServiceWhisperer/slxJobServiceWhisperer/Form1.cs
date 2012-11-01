using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sage.SData.Client.Core;
using Sage.SData.Client.Atom;
using Sage.SData.Client.Extensions;
using Sage.SData.Client.Framework;
using System.Net;
using System.IO;
using Newtonsoft.Json;


namespace slxJobServiceWhisperer
{
    public partial class frmJobs : Form
    {
        public frmJobs()
        {
            InitializeComponent();
        }

        private void frmJobs_Load(object sender, EventArgs e)
        {

            lstJobs.Items.Clear();
            lstExecutions.Items.Clear();
            lstTriggers.Items.Clear();

            //in here I am just going to load everything up... Jobs, then Triggers, then Executions.
            var service = new SDataService("http://localhost:3333/sdata/$app/scheduling/-/", "admin","");
            #region "jobs loading"
            //Load the jobs
            var request = new SDataResourceCollectionRequest(service)
                {
                    ResourceKind = "jobs"
                };
            txtURI.Text = request.ToString();
            foreach (var entry in request.Read().Entries)
            {
                var job = entry.GetSDataPayload();
                lstJobs.Items.Add(new Item(job.Key, job.Key, job.Key, job.Key, "", "", (AtomEntry)entry, ""));
            }
            #endregion "jobs loading"

            #region "trigger loading"
            request.ResourceKind = "triggers";
            
            txtURI.Text += request.ToString();
            foreach (var entry in request.Read().Entries)
            {
                var trigger = entry.GetSDataPayload();
                lstTriggers.Items.Add(new Item(trigger.Key, trigger.Key, trigger.Values["jobId"].ToString(), trigger.Key, "", trigger.Key, entry,""));
            }
            #endregion "trigger loading"

            #region "executions loading"
            request.ResourceKind = "executions";

            txtURI.Text += ";" + request.ToString();
            foreach (var entry in request.Read().Entries)
            {
                var execution = entry.GetSDataPayload();
                lstExecutions.Items.Add(new Item(execution.Key + "-" + execution.Values["progress"] + "%", execution.Key, execution.Values["jobId"].ToString(), execution.Key, execution.Key, execution.Values["triggerId"].ToString(), entry,""));
            }
            #endregion "executions loading"

        }

        private void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //need to the key from the selected Job.                        
            Item selecteditem = (Item)lstJobs.Items[lstJobs.SelectedIndex];
            if (lstJobs.SelectedIndex != -1)
            {
                //the button run only sets the paramters for the Sage.Platform.DynamicMethod.DynamicMethodJob..
                //so I have to limit the clickability of it ...
                if (selecteditem.Key == "Sage.Platform.DynamicMethod.DynamicMethodJob")
                {
                    btnRunMe.Enabled = true;
                }
                else
                {
                    btnRunMe.Enabled = false;
                }
                //show the details in the rtb
                LoadRTBFromEntryValues(selecteditem.Entry);

                //this just filters the triggers and clears the executions (just Jason's UI idea - could be different..)
                lstTriggers.Items.Clear();
                lstExecutions.Items.Clear();
                //this could be made into a reusable method, but for ease of learning I am repeating it here (copied from frmJobs_Load):
                #region "trigger loading"
                //had to copy these two lines too
                var service = new SDataService("http://localhost:3333/sdata/$app/scheduling/-/", "admin", "");
                var request = new SDataResourceCollectionRequest(service)
                {
                    ResourceKind = "triggers",
                    QueryValues =
                        {
                            {"where", "jobId eq '" + selecteditem.Job + "'"}
                        }
                };

                txtURI.Text = request.ToString();
                foreach (var entry in request.Read().Entries)
                {
                    var trigger = entry.GetSDataPayload();
                    lstTriggers.Items.Add(new Item(trigger.Key, trigger.Key, trigger.Values["jobId"].ToString(), trigger.Key, "", trigger.Key, entry, ""));
                }
                #endregion "trigger loading"
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            frmJobs_Load(sender, e);
        }

        private void lstTriggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //always need to make sure an item was selected..
            if (lstTriggers.SelectedIndex != -1)
            {
                //very similar to loading the triggers when a user selects a job,
                //now we want to select the executions for the trigger the user picked...

                //need to the key from the selected trigger.                        
                Item selecteditem = (Item)lstTriggers.Items[lstTriggers.SelectedIndex];
                LoadRTBFromEntryValues(selecteditem.Entry);

                //this just filters the executions and clears the executions (just Jason's UI idea - could be different..)
                lstExecutions.Items.Clear();
                //this could be made into a reusable method, but for ease of learning I am repeating it here (copied from frmJobs_Load):
                #region "execution loading"
                //had to copy these two lines too
                var service = new SDataService("http://localhost:3333/sdata/$app/scheduling/-/", "admin", "");
                var request = new SDataResourceCollectionRequest(service)
                {
                    ResourceKind = "executions",
                    QueryValues =
                        {
                            {"where", "triggerId eq '" + selecteditem.Trigger + "'"}
                        }
                };

                txtURI.Text = request.ToString();
                foreach (var entry in request.Read().Entries)
                {
                    var execution = entry.GetSDataPayload();
                    lstExecutions.Items.Add(new Item(execution.Key, execution.Key, execution.Values["jobId"].ToString(), execution.Key, execution.Key, execution.Values["triggerId"].ToString(), entry,""));
                }
                #endregion "execution loading"
            }

        }

        //just a method to load the rich text box from the entry's payload.Values

        private void LoadRTBFromEntryValues(AtomEntry entry)
        {
            rtbDetails.Text = "";
            var payload = entry.GetSDataPayload();
            foreach (var val in payload.Values)
	        {
                 rtbDetails.Text += val.Key + "-" + val.Value + "\n";		 
	        }
        }

        private void lstExecutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExecutions.SelectedIndex != -1)
            {

                //need to the key from the selected trigger.                        
                Item selecteditem = (Item)lstExecutions.Items[lstExecutions.SelectedIndex];
                LoadRTBFromEntryValues(selecteditem.Entry);
            }
        }

        private void btnRunMe_Click(object sender, EventArgs e)
        {


            if (lstJobs.SelectedIndex > -1)
            {
                //need to the key from the selected trigger.                        
                Item selecteditem = (Item)lstJobs.Items[lstJobs.SelectedIndex];


                var testuri = new SDataUri("http://localhost:3333/sdata/$app/scheduling/-");
                testuri.CollectionType = "jobs";
                testuri.CollectionPredicate = "'Sage.Platform.DynamicMethod.DynamicMethodJob'";
                testuri.ServiceMethod = "trigger";

                //right out of the introduction to job service white paper.

                var json = @"
            {
                request: {
                        parameters: [
                                {name: 'EntityId', value: 'AGHEA0002669'},
                                {name: 'MethodName', value: 'Account.GetTicketStats'}    
                                ]
                        }
            }";
          
                
                var request = WebRequest.Create(testuri.ToString());
                request.Credentials = new NetworkCredential("admin", "");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(json);
                }
                var response = request.GetResponse();

                Stream receiveStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[256];
                // Reads 256 characters at a time.     
                int count = readStream.Read(read, 0, 256);
                var content = "";
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    content += str;
                    count = readStream.Read(read, 0, 256);
                }
                rtbDetails.Text = content;
                //this is going to be a really bad way to get the trigger.
                string trigger = content.Substring(content.IndexOf("<triggerId>") + 11, content.IndexOf("</triggerId>") - content.IndexOf("<triggerId>") - 11);
                LoadTriggersbyId(trigger);
                // Releases the resources of the response.
                response.Close();
                // Releases the resources of the Stream.
                readStream.Close();
                txtURI.Text = testuri.ToString();
                
            }
        
        }

        private void LoadTriggersbyId(string triggerid)
        {
            //this just filters the triggers and clears the executions (just Jason's UI idea - could be different..)
            lstTriggers.Items.Clear();
            lstExecutions.Items.Clear();
            //this could be made into a reusable method, but for ease of learning I am repeating it here (copied from frmJobs_Load):
            #region "trigger loading"
            //had to copy these two lines too
            var service = new SDataService("http://localhost:3333/sdata/$app/scheduling/-/", "admin", "");
            var request = new SDataSingleResourceRequest(service)
            {
                ResourceKind = "triggers",
                ResourceSelector = "'" + triggerid + "'"
            };

            txtURI.Text = request.ToString();
            var entry= request.Read();
            var trigger = entry.GetSDataPayload();

                       lstTriggers.Items.Add(new Item(trigger.Key, trigger.Key, trigger.Values["jobId"].ToString(), trigger.Key, "", trigger.Key, entry, triggerid));
        #endregion "trigger loading"
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            if (lstExecutions.SelectedIndex != -1)
            {
                //need to the key from the selected trigger.                        
                Item selecteditem = (Item)lstExecutions.Items[lstExecutions.SelectedIndex];
                    var testuri = new SDataUri("http://localhost:3333/sdata/$app/scheduling/-/") { CollectionType = "executions" };
                    testuri.CollectionPredicate = "'" + selecteditem.Value + "'";
              
                    testuri.AppendPath("result");

                    var request = new SDataRequest(testuri.ToString()) { UserName = "admin" };
                    var response = request.GetResponse();
                    if (response.ContentType != null)
                    {
                           rtbDetails.Text = (string)response.Content;
                    }
                 
                     txtURI.Text = testuri.ToString();
        
            }
        }
    }

    //simple item class from:
    //http://social.msdn.microsoft.com/forums/en-US/winforms/thread/c7a82a6a-763e-424b-84e0-496caa9cfb4d/
    public class Item
    {
        public string Name;
        public string Value;
        public string Job;
        public string Key;
        public string Execution;
        public string Trigger;
        public AtomEntry Entry;
        public string triggerKey;

        public Item(string name, string value, string job, string key, string execution, string trigger, AtomEntry entry, string triggerkey)
        {
            Name = name; Value = value; Job = job; Key = key; Execution = execution; Trigger = trigger; Entry = entry; triggerKey = triggerkey;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }


}
