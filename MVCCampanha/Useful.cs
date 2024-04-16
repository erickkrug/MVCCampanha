//using System.Formats.Tar;

//namespace MVCCampanha
//{
//    public class Useful
//    {

//        private void BtnExportFile_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (txtFile.Text != "")
//                {
//                    lblAtendimentosInseridos.Text = "";
//                    lblFuncionarioInserido.Text = "";
//                    //var ListIds = readXlsx.ReadExcel(txtFile.Text);
//                    db.DeleteFuncionario();
//                    int countID = Dt.InsertAtend(txtFile.Text);
//                    lblFuncionarioInserido.Text = countID.ToString();
//                    if (countID > 0)
//                        MessageBox.Show("Os valores foram inseridos", "Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    else
//                        MessageBox.Show("Ocorreu um erro ao inserir os valores", "Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    BtnExportFile.Enabled = true;
//                    groupBoxAtend.Visible = true;

//                }
//                else
//                {
//                    MessageBox.Show("Selecione o arquivo!", "Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    BtnExportFile.Enabled = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                BtnExportFile.Enabled = true;
//                MessageBox.Show("Ocorreu um erro", "Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }


//    }
//}
