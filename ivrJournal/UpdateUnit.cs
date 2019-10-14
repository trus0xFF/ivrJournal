using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ivrJournal
{
    class UpdateUnit
    {
        private SQLDBConnect conSql;
        public UpdateUnit()
        {
            conSql = new SQLDBConnect();
        }

        public void DoUpdate()
        {
            // Проверяем версию базы данных
            String databaseVersion = conSql.GetSystemValue("Database Version");
            // Вносим соответствующие корректировки в базу данных
            if (databaseVersion == "1.0")
            {
                Updating_1_0();
                databaseVersion = "1.1";
            }

            if (databaseVersion == "1.1")
            {
                Updating_1_1();
                databaseVersion = "1.2";
            }

            if (databaseVersion == "1.2")
            {
                Updating_1_2();
                databaseVersion = "1.3";
            }

            if (databaseVersion == "1.3")
            {
                Updating_1_3();
                databaseVersion = "1.4";
            }

            if (databaseVersion == "1.4")
            {
                Updating_1_4();
                databaseVersion = "1.5";
            }

            if (databaseVersion == "1.5")
            {
                Updating_1_5();
                databaseVersion = "1.6";
            }

            string departmentID = conSql.GetSystemValue("Department");
            if (departmentID == null)
            {
                ChooseDepartmentForm frmChooseDepartment = new ChooseDepartmentForm();
                frmChooseDepartment.ShowDialog();
            }
        }

        private void Updating_1_0()
        {
            
            try
            {
                conSql.DoQuery("ALTER TABLE spec_psycho ADD date_doc DATETIME, name VARCHAR(50)");
                conSql.DoQuery("ALTER TABLE penalty ALTER COLUMN reason varchar(255)");
                conSql.DoQuery("ALTER TABLE penalty ALTER COLUMN removal varchar(255)");
                conSql.DoQuery("DROP TABLE system");
                conSql.DoQuery("CREATE TABLE system (id COUNTER not null primary key, name VARCHAR(50), system_value VARCHAR(50))");
                conSql.DoQuery("INSERT INTO system (id, name, system_value) values (1, 'Database Version', '1.1')");
                conSql.DoQuery("UPDATE system SET system_value = '1.1' WHERE (name = 'Database Version')");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при обновлении 1.0");
                return;
            }
        }

        private void Updating_1_1()
        {
            try
            {
                conSql.DoQuery("ALTER TABLE spec_psycho ADD type_doc int");
                conSql.DoQuery("UPDATE spec_psycho SET type_doc = '1'");
                conSql.DoQuery("ALTER TABLE spec ALTER COLUMN article varchar(255)");
                conSql.DoQuery("ALTER TABLE spec ALTER COLUMN court varchar(255)");
                conSql.DoQuery("ALTER TABLE spec ALTER COLUMN med_description text");
                conSql.DoQuery("ALTER TABLE spec ALTER COLUMN other text");
                conSql.DoQuery("UPDATE system SET system_value = '1.2' WHERE (name = 'Database Version')");

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при обновлении 1.1");
                return;
            }
        }

/*
        private void ExportDepartment()
        {
            String DataProvider = @"Microsoft.Jet.OLEDB.4.0";
            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();
            bldr.DataSource = this.dbPath; // Указываем путь
            bldr.Provider = DataProvider; // Указываем провайдера
            bldr.Add("Jet OLEDB:Database Password", "ivr32a");
            OleDbConnection OleDbCon = new OleDbConnection(bldr.ConnectionString);
            DataSet ds = new DataSet();

            OleDbCon.Open();

            (new OleDbDataAdapter("SELECT * FROM department", bldr.ConnectionString)).Fill(ds, "department");
            (new OleDbDataAdapter("SELECT * FROM enum_department_type", bldr.ConnectionString)).Fill(ds, "enum_department_type");

            XmlDataDocument xml = new XmlDataDocument(ds);
            OleDbCon.Close();

            ds.WriteXmlSchema("department.xsd");
            FileInfo f = new FileInfo("..\\ADONetDataSet.xml");
            xml.Save(f.FullName);
        }
*/

        private void Updating_1_2()
        {
            try
            {
                conSql.DoQuery("CREATE TABLE enum_department_type (id int not null primary key, name VARCHAR(255))");
                conSql.DoQuery("CREATE TABLE department (id varchar(15) not null primary key, name VARCHAR(255), higher VARCHAR(15), department_type_id int)");
                conSql.DoQuery("ALTER TABLE department ADD CONSTRAINT fk_dep_type FOREIGN KEY (department_type_id) REFERENCES enum_department_type (id)");
                conSql.DoQuery("ALTER TABLE employee ADD department_id varchar(15)");
                conSql.DoQuery("ALTER TABLE spr_party_number ADD department_id varchar(15)");

                conSql.DoQuery("ALTER TABLE prev_conv ADD period varchar(50)");

                conSql.DoQuery("DROP TABLE spr_condition");
                conSql.DoQuery("CREATE TABLE enum_condition (id int not null primary key, name VARCHAR(50), condition VARCHAR(50))");
                conSql.DoQuery("INSERT INTO enum_condition (id, name, condition) values (1, 'равно', '=')");
                conSql.DoQuery("INSERT INTO enum_condition (id, name, condition) values (2, 'больше', '<')");
                conSql.DoQuery("INSERT INTO enum_condition (id, name, condition) values (3, 'меньше', '>')");
                conSql.DoQuery("INSERT INTO enum_condition (id, name, condition) values (4, 'cодержит', 'LIKE')");

                FixDB_1_2();

                conSql.DoQuery("ALTER TABLE spec ADD CONSTRAINT fk_party_id  FOREIGN KEY (party_id) REFERENCES spr_party_number (id)");
                conSql.DoQuery("ALTER TABLE employee ADD CONSTRAINT fk_dep_id  FOREIGN KEY (department_id) REFERENCES department (id)");
                conSql.DoQuery("ALTER TABLE spr_party_number ADD CONSTRAINT fk_dep_id2  FOREIGN KEY (department_id) REFERENCES department (id)");
                conSql.DoQuery("ALTER TABLE spec_psycho ADD CONSTRAINT fk_spec_id  FOREIGN KEY (id_spec) REFERENCES spec (id)");

                conSql.DoQuery("UPDATE system SET system_value = '1.3' WHERE (name = 'Database Version')");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при обновлении 1.2");
                return;
            }
        }

        private void FixDB_1_2()
        {
            try
            {
                DataTable spec_psychoTable = conSql.GetDataTable("spec_psycho", "SELECT spec_psycho.id AS spec_psycho_id FROM spec_psycho LEFT JOIN spec ON spec.id=spec_psycho.id_spec WHERE (isnull(spec.last_name) And isnull(spec.first_name))");
                foreach (DataRow row in spec_psychoTable.Rows)
                {
                    conSql.DoQuery("DELETE * FROM spec_psycho WHERE (id=" + row["spec_psycho_id"].ToString() + ")");
                }

                string min_id_party = conSql.GetValue("min_id", "select min(id) as min_id from spr_party_number");
                conSql.DoQuery("UPDATE spec SET party_id = " + min_id_party + " WHERE (isnull(party_id) OR (party_id=0))");

                DataTable partyTable = conSql.GetDataTable("party", "SELECT spec.party_id AS party_id FROM spec LEFT JOIN spr_party_number ON spr_party_number.id=spec.party_id WHERE (isnull(spr_party_number.name))");
                foreach (DataRow row in partyTable.Rows)
                {
                    conSql.DoQuery("UPDATE spec SET party_id = " + min_id_party + " WHERE (party_id=" + row["party_id"].ToString() + ")");
                }

                DataTable prev_convTable = conSql.GetDataTable("prev_conv", "SELECT * FROM prev_conv");
                foreach (DataRow row in prev_convTable.Rows)
                {
                    conSql.DoQuery("UPDATE prev_conv SET period = '" + row["period_years"].ToString() + " лет " + row["period_months"].ToString() + " мес " + row["period_days"].ToString() + " дней " + "' WHERE (id=" + row["id"].ToString() + ")");
                }

                //Удаляем лишние записи в таблице spec_psycho
                //                conSql.DoQuery("DELETE * FROM spec_psycho WHERE (id=6 OR id=7 OR id=8 OR id=9)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при изменении структуры базы данных, обновление 1.2");
                return;
            }
        }

        private void Updating_1_3()
        {
            try
            {
                conSql.DoQuery(@"CREATE TABLE results 
                                (id COUNTER not null PRIMARY KEY, 
                                id_spec INT, 
                                result_date DATETIME, 
                                content MEMO, 
                                description VARCHAR(255) )");
                conSql.DoQuery("ALTER TABLE results ADD CONSTRAINT fk_spec_id2  FOREIGN KEY (id_spec) REFERENCES spec (id)");

                conSql.DoQuery("ALTER TABLE prev_conv ALTER COLUMN text_prev text");
                conSql.DoQuery("ALTER TABLE prev_conv ALTER COLUMN article varchar(255)");

                conSql.DoQuery("UPDATE system SET system_value = '1.4' WHERE (name = 'Database Version')");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при изменении структуры базы данных, обновление 1.3");
                return;
            }
        }

        private void Updating_1_4()
        {
            try
            {
                conSql.DoQuery("UPDATE stuctur_base SET spr = 'spr_party_number' WHERE (field_name = 'party_id')");
                conSql.DoQuery("UPDATE enum_condition SET condition = '>' WHERE (name = 'больше')");
                conSql.DoQuery("UPDATE enum_condition SET condition = '<' WHERE (name = 'меньше')");

                conSql.DoQuery("UPDATE system SET system_value = '1.5' WHERE (name = 'Database Version')");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при изменении структуры базы данных, обновление 1.4");
                return;
            }
        }

        private void Updating_1_5()
        {
            try
            {
                conSql.DoQuery("DELETE * FROM users WHERE user_rol_id NOT IN (SELECT DISTINCT id FROM spr_user_rol)");
                conSql.DoQuery("DELETE * FROM user_rol_access WHERE id_user_rol NOT IN (SELECT DISTINCT id FROM spr_user_rol)");
                conSql.DoQuery("DELETE * FROM user_rol_access WHERE id_user_controls NOT IN (SELECT DISTINCT id FROM spr_user_controls)");
                
                conSql.DoQuery("ALTER TABLE users ALTER COLUMN user_rol_id INT NOT NULL");
                

                conSql.DoQuery("ALTER TABLE users ADD CONSTRAINT fk_user_rol_id  FOREIGN KEY (user_rol_id) REFERENCES spr_user_rol (id)");
                conSql.DoQuery("ALTER TABLE user_rol_access ADD CONSTRAINT fk_user_rol_id2  FOREIGN KEY (id_user_rol) REFERENCES spr_user_rol (id)");
                conSql.DoQuery("ALTER TABLE user_rol_access ADD CONSTRAINT fk_user_controls_id  FOREIGN KEY (id_user_controls) REFERENCES spr_user_controls (id)");

                conSql.DoQuery("UPDATE system SET system_value = '1.6' WHERE (name = 'Database Version')");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при изменении структуры базы данных, обновление 1.5");
                return;
            }
        }

    }
}
