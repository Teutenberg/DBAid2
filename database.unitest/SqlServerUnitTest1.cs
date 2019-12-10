using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace database.unitest
{
    [TestClass()]
    public class SqlServerUnitTest1 : SqlDatabaseTestClass
    {

        public SqlServerUnitTest1()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_clean_stringTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerUnitTest1));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_datetimeoffsetTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_instance_guidTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_product_versionTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction audit_get_database_last_accessTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_chart_capacity_fgTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_agentjobTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition7;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_alwaysonTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition8;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_backupTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition9;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_databaseTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition10;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_integrityTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition11;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_logshippingTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition12;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_check_mirroringTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition13;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_inventory_agentjobTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition14;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_inventory_alwaysonTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition15;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_inventory_databaseTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition16;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction collector_get_agentjob_historyTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition17;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction collector_get_backup_historyTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition18;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction collector_get_capacity_dbTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition19;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction collector_get_database_ciTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition20;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction collector_get_errorlog_historyTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition21;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction collector_get_instance_ciTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition22;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction datamart_process_get_dataTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition23;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CommandExecuteTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition24;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DatabaseBackupTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition25;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DatabaseIntegrityCheckTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition26;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_IndexOptimizeTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition27;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_sp_WhoIsActiveTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition28;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction health_get_block_process_historyTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition29;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction health_get_dead_lock_historyTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition30;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction health_get_file_io_performanceTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition31;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction health_get_index_fragmentationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition32;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction health_get_statistic_stateTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition33;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction health_get_transaction_logTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition34;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_delete_system_historyTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition35;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_execute_foreach_dbTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition36;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_generate_database_permission_scriptTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition37;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_generate_login_scriptTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition38;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_generate_secretTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition39;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_instance_tagTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition40;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_set_ag_agent_job_stateTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition41;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_set_default_agentjob_configurationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition42;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_set_default_db_performance_configurationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition43;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_hexadecimalTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition44;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction system_get_split_stringTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition45;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction checkmk_ag_role_change_datetimeTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition46;
            this.system_get_clean_stringTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_get_datetimeoffsetTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_get_instance_guidTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_get_product_versionTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.audit_get_database_last_accessTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_chart_capacity_fgTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_agentjobTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_alwaysonTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_backupTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_databaseTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_integrityTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_logshippingTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_check_mirroringTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_inventory_agentjobTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_inventory_alwaysonTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_inventory_databaseTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.collector_get_agentjob_historyTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.collector_get_backup_historyTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.collector_get_capacity_dbTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.collector_get_database_ciTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.collector_get_errorlog_historyTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.collector_get_instance_ciTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.datamart_process_get_dataTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CommandExecuteTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DatabaseBackupTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DatabaseIntegrityCheckTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_IndexOptimizeTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_sp_WhoIsActiveTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.health_get_block_process_historyTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.health_get_dead_lock_historyTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.health_get_file_io_performanceTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.health_get_index_fragmentationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.health_get_statistic_stateTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.health_get_transaction_logTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_delete_system_historyTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_execute_foreach_dbTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_generate_database_permission_scriptTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_generate_login_scriptTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_generate_secretTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_get_instance_tagTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_set_ag_agent_job_stateTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_set_default_agentjob_configurationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_set_default_db_performance_configurationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_get_hexadecimalTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.system_get_split_stringTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.checkmk_ag_role_change_datetimeTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            system_get_clean_stringTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_get_datetimeoffsetTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_get_instance_guidTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_get_product_versionTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            audit_get_database_last_accessTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_chart_capacity_fgTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_agentjobTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_alwaysonTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition8 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_backupTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition9 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_databaseTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition10 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_integrityTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition11 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_logshippingTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition12 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_check_mirroringTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition13 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_inventory_agentjobTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition14 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_inventory_alwaysonTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition15 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_inventory_databaseTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition16 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            collector_get_agentjob_historyTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition17 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            collector_get_backup_historyTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition18 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            collector_get_capacity_dbTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition19 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            collector_get_database_ciTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition20 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            collector_get_errorlog_historyTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition21 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            collector_get_instance_ciTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition22 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            datamart_process_get_dataTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition23 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CommandExecuteTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition24 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DatabaseBackupTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition25 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DatabaseIntegrityCheckTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition26 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_IndexOptimizeTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition27 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_sp_WhoIsActiveTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition28 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            health_get_block_process_historyTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition29 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            health_get_dead_lock_historyTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition30 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            health_get_file_io_performanceTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition31 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            health_get_index_fragmentationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition32 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            health_get_statistic_stateTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition33 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            health_get_transaction_logTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition34 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_delete_system_historyTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition35 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_execute_foreach_dbTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition36 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_generate_database_permission_scriptTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition37 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_generate_login_scriptTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition38 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_generate_secretTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition39 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_get_instance_tagTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition40 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_set_ag_agent_job_stateTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition41 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_set_default_agentjob_configurationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition42 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_set_default_db_performance_configurationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition43 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_get_hexadecimalTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition44 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            system_get_split_stringTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition45 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            checkmk_ag_role_change_datetimeTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition46 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // system_get_clean_stringTestData
            // 
            this.system_get_clean_stringTestData.PosttestAction = null;
            this.system_get_clean_stringTestData.PretestAction = null;
            this.system_get_clean_stringTestData.TestAction = system_get_clean_stringTest_TestAction;
            // 
            // system_get_clean_stringTest_TestAction
            // 
            system_get_clean_stringTest_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(system_get_clean_stringTest_TestAction, "system_get_clean_stringTest_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // system_get_datetimeoffsetTestData
            // 
            this.system_get_datetimeoffsetTestData.PosttestAction = null;
            this.system_get_datetimeoffsetTestData.PretestAction = null;
            this.system_get_datetimeoffsetTestData.TestAction = system_get_datetimeoffsetTest_TestAction;
            // 
            // system_get_datetimeoffsetTest_TestAction
            // 
            system_get_datetimeoffsetTest_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(system_get_datetimeoffsetTest_TestAction, "system_get_datetimeoffsetTest_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // system_get_instance_guidTestData
            // 
            this.system_get_instance_guidTestData.PosttestAction = null;
            this.system_get_instance_guidTestData.PretestAction = null;
            this.system_get_instance_guidTestData.TestAction = system_get_instance_guidTest_TestAction;
            // 
            // system_get_instance_guidTest_TestAction
            // 
            system_get_instance_guidTest_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(system_get_instance_guidTest_TestAction, "system_get_instance_guidTest_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // system_get_product_versionTestData
            // 
            this.system_get_product_versionTestData.PosttestAction = null;
            this.system_get_product_versionTestData.PretestAction = null;
            this.system_get_product_versionTestData.TestAction = system_get_product_versionTest_TestAction;
            // 
            // system_get_product_versionTest_TestAction
            // 
            system_get_product_versionTest_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(system_get_product_versionTest_TestAction, "system_get_product_versionTest_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
            // 
            // audit_get_database_last_accessTestData
            // 
            this.audit_get_database_last_accessTestData.PosttestAction = null;
            this.audit_get_database_last_accessTestData.PretestAction = null;
            this.audit_get_database_last_accessTestData.TestAction = audit_get_database_last_accessTest_TestAction;
            // 
            // audit_get_database_last_accessTest_TestAction
            // 
            audit_get_database_last_accessTest_TestAction.Conditions.Add(inconclusiveCondition5);
            resources.ApplyResources(audit_get_database_last_accessTest_TestAction, "audit_get_database_last_accessTest_TestAction");
            // 
            // inconclusiveCondition5
            // 
            inconclusiveCondition5.Enabled = true;
            inconclusiveCondition5.Name = "inconclusiveCondition5";
            // 
            // checkmk_chart_capacity_fgTestData
            // 
            this.checkmk_chart_capacity_fgTestData.PosttestAction = null;
            this.checkmk_chart_capacity_fgTestData.PretestAction = null;
            this.checkmk_chart_capacity_fgTestData.TestAction = checkmk_chart_capacity_fgTest_TestAction;
            // 
            // checkmk_chart_capacity_fgTest_TestAction
            // 
            checkmk_chart_capacity_fgTest_TestAction.Conditions.Add(inconclusiveCondition6);
            resources.ApplyResources(checkmk_chart_capacity_fgTest_TestAction, "checkmk_chart_capacity_fgTest_TestAction");
            // 
            // inconclusiveCondition6
            // 
            inconclusiveCondition6.Enabled = true;
            inconclusiveCondition6.Name = "inconclusiveCondition6";
            // 
            // checkmk_check_agentjobTestData
            // 
            this.checkmk_check_agentjobTestData.PosttestAction = null;
            this.checkmk_check_agentjobTestData.PretestAction = null;
            this.checkmk_check_agentjobTestData.TestAction = checkmk_check_agentjobTest_TestAction;
            // 
            // checkmk_check_agentjobTest_TestAction
            // 
            checkmk_check_agentjobTest_TestAction.Conditions.Add(inconclusiveCondition7);
            resources.ApplyResources(checkmk_check_agentjobTest_TestAction, "checkmk_check_agentjobTest_TestAction");
            // 
            // inconclusiveCondition7
            // 
            inconclusiveCondition7.Enabled = true;
            inconclusiveCondition7.Name = "inconclusiveCondition7";
            // 
            // checkmk_check_alwaysonTestData
            // 
            this.checkmk_check_alwaysonTestData.PosttestAction = null;
            this.checkmk_check_alwaysonTestData.PretestAction = null;
            this.checkmk_check_alwaysonTestData.TestAction = checkmk_check_alwaysonTest_TestAction;
            // 
            // checkmk_check_alwaysonTest_TestAction
            // 
            checkmk_check_alwaysonTest_TestAction.Conditions.Add(inconclusiveCondition8);
            resources.ApplyResources(checkmk_check_alwaysonTest_TestAction, "checkmk_check_alwaysonTest_TestAction");
            // 
            // inconclusiveCondition8
            // 
            inconclusiveCondition8.Enabled = true;
            inconclusiveCondition8.Name = "inconclusiveCondition8";
            // 
            // checkmk_check_backupTestData
            // 
            this.checkmk_check_backupTestData.PosttestAction = null;
            this.checkmk_check_backupTestData.PretestAction = null;
            this.checkmk_check_backupTestData.TestAction = checkmk_check_backupTest_TestAction;
            // 
            // checkmk_check_backupTest_TestAction
            // 
            checkmk_check_backupTest_TestAction.Conditions.Add(inconclusiveCondition9);
            resources.ApplyResources(checkmk_check_backupTest_TestAction, "checkmk_check_backupTest_TestAction");
            // 
            // inconclusiveCondition9
            // 
            inconclusiveCondition9.Enabled = true;
            inconclusiveCondition9.Name = "inconclusiveCondition9";
            // 
            // checkmk_check_databaseTestData
            // 
            this.checkmk_check_databaseTestData.PosttestAction = null;
            this.checkmk_check_databaseTestData.PretestAction = null;
            this.checkmk_check_databaseTestData.TestAction = checkmk_check_databaseTest_TestAction;
            // 
            // checkmk_check_databaseTest_TestAction
            // 
            checkmk_check_databaseTest_TestAction.Conditions.Add(inconclusiveCondition10);
            resources.ApplyResources(checkmk_check_databaseTest_TestAction, "checkmk_check_databaseTest_TestAction");
            // 
            // inconclusiveCondition10
            // 
            inconclusiveCondition10.Enabled = true;
            inconclusiveCondition10.Name = "inconclusiveCondition10";
            // 
            // checkmk_check_integrityTestData
            // 
            this.checkmk_check_integrityTestData.PosttestAction = null;
            this.checkmk_check_integrityTestData.PretestAction = null;
            this.checkmk_check_integrityTestData.TestAction = checkmk_check_integrityTest_TestAction;
            // 
            // checkmk_check_integrityTest_TestAction
            // 
            checkmk_check_integrityTest_TestAction.Conditions.Add(inconclusiveCondition11);
            resources.ApplyResources(checkmk_check_integrityTest_TestAction, "checkmk_check_integrityTest_TestAction");
            // 
            // inconclusiveCondition11
            // 
            inconclusiveCondition11.Enabled = true;
            inconclusiveCondition11.Name = "inconclusiveCondition11";
            // 
            // checkmk_check_logshippingTestData
            // 
            this.checkmk_check_logshippingTestData.PosttestAction = null;
            this.checkmk_check_logshippingTestData.PretestAction = null;
            this.checkmk_check_logshippingTestData.TestAction = checkmk_check_logshippingTest_TestAction;
            // 
            // checkmk_check_logshippingTest_TestAction
            // 
            checkmk_check_logshippingTest_TestAction.Conditions.Add(inconclusiveCondition12);
            resources.ApplyResources(checkmk_check_logshippingTest_TestAction, "checkmk_check_logshippingTest_TestAction");
            // 
            // inconclusiveCondition12
            // 
            inconclusiveCondition12.Enabled = true;
            inconclusiveCondition12.Name = "inconclusiveCondition12";
            // 
            // checkmk_check_mirroringTestData
            // 
            this.checkmk_check_mirroringTestData.PosttestAction = null;
            this.checkmk_check_mirroringTestData.PretestAction = null;
            this.checkmk_check_mirroringTestData.TestAction = checkmk_check_mirroringTest_TestAction;
            // 
            // checkmk_check_mirroringTest_TestAction
            // 
            checkmk_check_mirroringTest_TestAction.Conditions.Add(inconclusiveCondition13);
            resources.ApplyResources(checkmk_check_mirroringTest_TestAction, "checkmk_check_mirroringTest_TestAction");
            // 
            // inconclusiveCondition13
            // 
            inconclusiveCondition13.Enabled = true;
            inconclusiveCondition13.Name = "inconclusiveCondition13";
            // 
            // checkmk_inventory_agentjobTestData
            // 
            this.checkmk_inventory_agentjobTestData.PosttestAction = null;
            this.checkmk_inventory_agentjobTestData.PretestAction = null;
            this.checkmk_inventory_agentjobTestData.TestAction = checkmk_inventory_agentjobTest_TestAction;
            // 
            // checkmk_inventory_agentjobTest_TestAction
            // 
            checkmk_inventory_agentjobTest_TestAction.Conditions.Add(inconclusiveCondition14);
            resources.ApplyResources(checkmk_inventory_agentjobTest_TestAction, "checkmk_inventory_agentjobTest_TestAction");
            // 
            // inconclusiveCondition14
            // 
            inconclusiveCondition14.Enabled = true;
            inconclusiveCondition14.Name = "inconclusiveCondition14";
            // 
            // checkmk_inventory_alwaysonTestData
            // 
            this.checkmk_inventory_alwaysonTestData.PosttestAction = null;
            this.checkmk_inventory_alwaysonTestData.PretestAction = null;
            this.checkmk_inventory_alwaysonTestData.TestAction = checkmk_inventory_alwaysonTest_TestAction;
            // 
            // checkmk_inventory_alwaysonTest_TestAction
            // 
            checkmk_inventory_alwaysonTest_TestAction.Conditions.Add(inconclusiveCondition15);
            resources.ApplyResources(checkmk_inventory_alwaysonTest_TestAction, "checkmk_inventory_alwaysonTest_TestAction");
            // 
            // inconclusiveCondition15
            // 
            inconclusiveCondition15.Enabled = true;
            inconclusiveCondition15.Name = "inconclusiveCondition15";
            // 
            // checkmk_inventory_databaseTestData
            // 
            this.checkmk_inventory_databaseTestData.PosttestAction = null;
            this.checkmk_inventory_databaseTestData.PretestAction = null;
            this.checkmk_inventory_databaseTestData.TestAction = checkmk_inventory_databaseTest_TestAction;
            // 
            // checkmk_inventory_databaseTest_TestAction
            // 
            checkmk_inventory_databaseTest_TestAction.Conditions.Add(inconclusiveCondition16);
            resources.ApplyResources(checkmk_inventory_databaseTest_TestAction, "checkmk_inventory_databaseTest_TestAction");
            // 
            // inconclusiveCondition16
            // 
            inconclusiveCondition16.Enabled = true;
            inconclusiveCondition16.Name = "inconclusiveCondition16";
            // 
            // collector_get_agentjob_historyTestData
            // 
            this.collector_get_agentjob_historyTestData.PosttestAction = null;
            this.collector_get_agentjob_historyTestData.PretestAction = null;
            this.collector_get_agentjob_historyTestData.TestAction = collector_get_agentjob_historyTest_TestAction;
            // 
            // collector_get_agentjob_historyTest_TestAction
            // 
            collector_get_agentjob_historyTest_TestAction.Conditions.Add(inconclusiveCondition17);
            resources.ApplyResources(collector_get_agentjob_historyTest_TestAction, "collector_get_agentjob_historyTest_TestAction");
            // 
            // inconclusiveCondition17
            // 
            inconclusiveCondition17.Enabled = true;
            inconclusiveCondition17.Name = "inconclusiveCondition17";
            // 
            // collector_get_backup_historyTestData
            // 
            this.collector_get_backup_historyTestData.PosttestAction = null;
            this.collector_get_backup_historyTestData.PretestAction = null;
            this.collector_get_backup_historyTestData.TestAction = collector_get_backup_historyTest_TestAction;
            // 
            // collector_get_backup_historyTest_TestAction
            // 
            collector_get_backup_historyTest_TestAction.Conditions.Add(inconclusiveCondition18);
            resources.ApplyResources(collector_get_backup_historyTest_TestAction, "collector_get_backup_historyTest_TestAction");
            // 
            // inconclusiveCondition18
            // 
            inconclusiveCondition18.Enabled = true;
            inconclusiveCondition18.Name = "inconclusiveCondition18";
            // 
            // collector_get_capacity_dbTestData
            // 
            this.collector_get_capacity_dbTestData.PosttestAction = null;
            this.collector_get_capacity_dbTestData.PretestAction = null;
            this.collector_get_capacity_dbTestData.TestAction = collector_get_capacity_dbTest_TestAction;
            // 
            // collector_get_capacity_dbTest_TestAction
            // 
            collector_get_capacity_dbTest_TestAction.Conditions.Add(inconclusiveCondition19);
            resources.ApplyResources(collector_get_capacity_dbTest_TestAction, "collector_get_capacity_dbTest_TestAction");
            // 
            // inconclusiveCondition19
            // 
            inconclusiveCondition19.Enabled = true;
            inconclusiveCondition19.Name = "inconclusiveCondition19";
            // 
            // collector_get_database_ciTestData
            // 
            this.collector_get_database_ciTestData.PosttestAction = null;
            this.collector_get_database_ciTestData.PretestAction = null;
            this.collector_get_database_ciTestData.TestAction = collector_get_database_ciTest_TestAction;
            // 
            // collector_get_database_ciTest_TestAction
            // 
            collector_get_database_ciTest_TestAction.Conditions.Add(inconclusiveCondition20);
            resources.ApplyResources(collector_get_database_ciTest_TestAction, "collector_get_database_ciTest_TestAction");
            // 
            // inconclusiveCondition20
            // 
            inconclusiveCondition20.Enabled = true;
            inconclusiveCondition20.Name = "inconclusiveCondition20";
            // 
            // collector_get_errorlog_historyTestData
            // 
            this.collector_get_errorlog_historyTestData.PosttestAction = null;
            this.collector_get_errorlog_historyTestData.PretestAction = null;
            this.collector_get_errorlog_historyTestData.TestAction = collector_get_errorlog_historyTest_TestAction;
            // 
            // collector_get_errorlog_historyTest_TestAction
            // 
            collector_get_errorlog_historyTest_TestAction.Conditions.Add(inconclusiveCondition21);
            resources.ApplyResources(collector_get_errorlog_historyTest_TestAction, "collector_get_errorlog_historyTest_TestAction");
            // 
            // inconclusiveCondition21
            // 
            inconclusiveCondition21.Enabled = true;
            inconclusiveCondition21.Name = "inconclusiveCondition21";
            // 
            // collector_get_instance_ciTestData
            // 
            this.collector_get_instance_ciTestData.PosttestAction = null;
            this.collector_get_instance_ciTestData.PretestAction = null;
            this.collector_get_instance_ciTestData.TestAction = collector_get_instance_ciTest_TestAction;
            // 
            // collector_get_instance_ciTest_TestAction
            // 
            collector_get_instance_ciTest_TestAction.Conditions.Add(inconclusiveCondition22);
            resources.ApplyResources(collector_get_instance_ciTest_TestAction, "collector_get_instance_ciTest_TestAction");
            // 
            // inconclusiveCondition22
            // 
            inconclusiveCondition22.Enabled = true;
            inconclusiveCondition22.Name = "inconclusiveCondition22";
            // 
            // datamart_process_get_dataTestData
            // 
            this.datamart_process_get_dataTestData.PosttestAction = null;
            this.datamart_process_get_dataTestData.PretestAction = null;
            this.datamart_process_get_dataTestData.TestAction = datamart_process_get_dataTest_TestAction;
            // 
            // datamart_process_get_dataTest_TestAction
            // 
            datamart_process_get_dataTest_TestAction.Conditions.Add(inconclusiveCondition23);
            resources.ApplyResources(datamart_process_get_dataTest_TestAction, "datamart_process_get_dataTest_TestAction");
            // 
            // inconclusiveCondition23
            // 
            inconclusiveCondition23.Enabled = true;
            inconclusiveCondition23.Name = "inconclusiveCondition23";
            // 
            // dbo_CommandExecuteTestData
            // 
            this.dbo_CommandExecuteTestData.PosttestAction = null;
            this.dbo_CommandExecuteTestData.PretestAction = null;
            this.dbo_CommandExecuteTestData.TestAction = dbo_CommandExecuteTest_TestAction;
            // 
            // dbo_CommandExecuteTest_TestAction
            // 
            dbo_CommandExecuteTest_TestAction.Conditions.Add(inconclusiveCondition24);
            resources.ApplyResources(dbo_CommandExecuteTest_TestAction, "dbo_CommandExecuteTest_TestAction");
            // 
            // inconclusiveCondition24
            // 
            inconclusiveCondition24.Enabled = true;
            inconclusiveCondition24.Name = "inconclusiveCondition24";
            // 
            // dbo_DatabaseBackupTestData
            // 
            this.dbo_DatabaseBackupTestData.PosttestAction = null;
            this.dbo_DatabaseBackupTestData.PretestAction = null;
            this.dbo_DatabaseBackupTestData.TestAction = dbo_DatabaseBackupTest_TestAction;
            // 
            // dbo_DatabaseBackupTest_TestAction
            // 
            dbo_DatabaseBackupTest_TestAction.Conditions.Add(inconclusiveCondition25);
            resources.ApplyResources(dbo_DatabaseBackupTest_TestAction, "dbo_DatabaseBackupTest_TestAction");
            // 
            // inconclusiveCondition25
            // 
            inconclusiveCondition25.Enabled = true;
            inconclusiveCondition25.Name = "inconclusiveCondition25";
            // 
            // dbo_DatabaseIntegrityCheckTestData
            // 
            this.dbo_DatabaseIntegrityCheckTestData.PosttestAction = null;
            this.dbo_DatabaseIntegrityCheckTestData.PretestAction = null;
            this.dbo_DatabaseIntegrityCheckTestData.TestAction = dbo_DatabaseIntegrityCheckTest_TestAction;
            // 
            // dbo_DatabaseIntegrityCheckTest_TestAction
            // 
            dbo_DatabaseIntegrityCheckTest_TestAction.Conditions.Add(inconclusiveCondition26);
            resources.ApplyResources(dbo_DatabaseIntegrityCheckTest_TestAction, "dbo_DatabaseIntegrityCheckTest_TestAction");
            // 
            // inconclusiveCondition26
            // 
            inconclusiveCondition26.Enabled = true;
            inconclusiveCondition26.Name = "inconclusiveCondition26";
            // 
            // dbo_IndexOptimizeTestData
            // 
            this.dbo_IndexOptimizeTestData.PosttestAction = null;
            this.dbo_IndexOptimizeTestData.PretestAction = null;
            this.dbo_IndexOptimizeTestData.TestAction = dbo_IndexOptimizeTest_TestAction;
            // 
            // dbo_IndexOptimizeTest_TestAction
            // 
            dbo_IndexOptimizeTest_TestAction.Conditions.Add(inconclusiveCondition27);
            resources.ApplyResources(dbo_IndexOptimizeTest_TestAction, "dbo_IndexOptimizeTest_TestAction");
            // 
            // inconclusiveCondition27
            // 
            inconclusiveCondition27.Enabled = true;
            inconclusiveCondition27.Name = "inconclusiveCondition27";
            // 
            // dbo_sp_WhoIsActiveTestData
            // 
            this.dbo_sp_WhoIsActiveTestData.PosttestAction = null;
            this.dbo_sp_WhoIsActiveTestData.PretestAction = null;
            this.dbo_sp_WhoIsActiveTestData.TestAction = dbo_sp_WhoIsActiveTest_TestAction;
            // 
            // dbo_sp_WhoIsActiveTest_TestAction
            // 
            dbo_sp_WhoIsActiveTest_TestAction.Conditions.Add(inconclusiveCondition28);
            resources.ApplyResources(dbo_sp_WhoIsActiveTest_TestAction, "dbo_sp_WhoIsActiveTest_TestAction");
            // 
            // inconclusiveCondition28
            // 
            inconclusiveCondition28.Enabled = true;
            inconclusiveCondition28.Name = "inconclusiveCondition28";
            // 
            // health_get_block_process_historyTestData
            // 
            this.health_get_block_process_historyTestData.PosttestAction = null;
            this.health_get_block_process_historyTestData.PretestAction = null;
            this.health_get_block_process_historyTestData.TestAction = health_get_block_process_historyTest_TestAction;
            // 
            // health_get_block_process_historyTest_TestAction
            // 
            health_get_block_process_historyTest_TestAction.Conditions.Add(inconclusiveCondition29);
            resources.ApplyResources(health_get_block_process_historyTest_TestAction, "health_get_block_process_historyTest_TestAction");
            // 
            // inconclusiveCondition29
            // 
            inconclusiveCondition29.Enabled = true;
            inconclusiveCondition29.Name = "inconclusiveCondition29";
            // 
            // health_get_dead_lock_historyTestData
            // 
            this.health_get_dead_lock_historyTestData.PosttestAction = null;
            this.health_get_dead_lock_historyTestData.PretestAction = null;
            this.health_get_dead_lock_historyTestData.TestAction = health_get_dead_lock_historyTest_TestAction;
            // 
            // health_get_dead_lock_historyTest_TestAction
            // 
            health_get_dead_lock_historyTest_TestAction.Conditions.Add(inconclusiveCondition30);
            resources.ApplyResources(health_get_dead_lock_historyTest_TestAction, "health_get_dead_lock_historyTest_TestAction");
            // 
            // inconclusiveCondition30
            // 
            inconclusiveCondition30.Enabled = true;
            inconclusiveCondition30.Name = "inconclusiveCondition30";
            // 
            // health_get_file_io_performanceTestData
            // 
            this.health_get_file_io_performanceTestData.PosttestAction = null;
            this.health_get_file_io_performanceTestData.PretestAction = null;
            this.health_get_file_io_performanceTestData.TestAction = health_get_file_io_performanceTest_TestAction;
            // 
            // health_get_file_io_performanceTest_TestAction
            // 
            health_get_file_io_performanceTest_TestAction.Conditions.Add(inconclusiveCondition31);
            resources.ApplyResources(health_get_file_io_performanceTest_TestAction, "health_get_file_io_performanceTest_TestAction");
            // 
            // inconclusiveCondition31
            // 
            inconclusiveCondition31.Enabled = true;
            inconclusiveCondition31.Name = "inconclusiveCondition31";
            // 
            // health_get_index_fragmentationTestData
            // 
            this.health_get_index_fragmentationTestData.PosttestAction = null;
            this.health_get_index_fragmentationTestData.PretestAction = null;
            this.health_get_index_fragmentationTestData.TestAction = health_get_index_fragmentationTest_TestAction;
            // 
            // health_get_index_fragmentationTest_TestAction
            // 
            health_get_index_fragmentationTest_TestAction.Conditions.Add(inconclusiveCondition32);
            resources.ApplyResources(health_get_index_fragmentationTest_TestAction, "health_get_index_fragmentationTest_TestAction");
            // 
            // inconclusiveCondition32
            // 
            inconclusiveCondition32.Enabled = true;
            inconclusiveCondition32.Name = "inconclusiveCondition32";
            // 
            // health_get_statistic_stateTestData
            // 
            this.health_get_statistic_stateTestData.PosttestAction = null;
            this.health_get_statistic_stateTestData.PretestAction = null;
            this.health_get_statistic_stateTestData.TestAction = health_get_statistic_stateTest_TestAction;
            // 
            // health_get_statistic_stateTest_TestAction
            // 
            health_get_statistic_stateTest_TestAction.Conditions.Add(inconclusiveCondition33);
            resources.ApplyResources(health_get_statistic_stateTest_TestAction, "health_get_statistic_stateTest_TestAction");
            // 
            // inconclusiveCondition33
            // 
            inconclusiveCondition33.Enabled = true;
            inconclusiveCondition33.Name = "inconclusiveCondition33";
            // 
            // health_get_transaction_logTestData
            // 
            this.health_get_transaction_logTestData.PosttestAction = null;
            this.health_get_transaction_logTestData.PretestAction = null;
            this.health_get_transaction_logTestData.TestAction = health_get_transaction_logTest_TestAction;
            // 
            // health_get_transaction_logTest_TestAction
            // 
            health_get_transaction_logTest_TestAction.Conditions.Add(inconclusiveCondition34);
            resources.ApplyResources(health_get_transaction_logTest_TestAction, "health_get_transaction_logTest_TestAction");
            // 
            // inconclusiveCondition34
            // 
            inconclusiveCondition34.Enabled = true;
            inconclusiveCondition34.Name = "inconclusiveCondition34";
            // 
            // system_delete_system_historyTestData
            // 
            this.system_delete_system_historyTestData.PosttestAction = null;
            this.system_delete_system_historyTestData.PretestAction = null;
            this.system_delete_system_historyTestData.TestAction = system_delete_system_historyTest_TestAction;
            // 
            // system_delete_system_historyTest_TestAction
            // 
            system_delete_system_historyTest_TestAction.Conditions.Add(inconclusiveCondition35);
            resources.ApplyResources(system_delete_system_historyTest_TestAction, "system_delete_system_historyTest_TestAction");
            // 
            // inconclusiveCondition35
            // 
            inconclusiveCondition35.Enabled = true;
            inconclusiveCondition35.Name = "inconclusiveCondition35";
            // 
            // system_execute_foreach_dbTestData
            // 
            this.system_execute_foreach_dbTestData.PosttestAction = null;
            this.system_execute_foreach_dbTestData.PretestAction = null;
            this.system_execute_foreach_dbTestData.TestAction = system_execute_foreach_dbTest_TestAction;
            // 
            // system_execute_foreach_dbTest_TestAction
            // 
            system_execute_foreach_dbTest_TestAction.Conditions.Add(inconclusiveCondition36);
            resources.ApplyResources(system_execute_foreach_dbTest_TestAction, "system_execute_foreach_dbTest_TestAction");
            // 
            // inconclusiveCondition36
            // 
            inconclusiveCondition36.Enabled = true;
            inconclusiveCondition36.Name = "inconclusiveCondition36";
            // 
            // system_generate_database_permission_scriptTestData
            // 
            this.system_generate_database_permission_scriptTestData.PosttestAction = null;
            this.system_generate_database_permission_scriptTestData.PretestAction = null;
            this.system_generate_database_permission_scriptTestData.TestAction = system_generate_database_permission_scriptTest_TestAction;
            // 
            // system_generate_database_permission_scriptTest_TestAction
            // 
            system_generate_database_permission_scriptTest_TestAction.Conditions.Add(inconclusiveCondition37);
            resources.ApplyResources(system_generate_database_permission_scriptTest_TestAction, "system_generate_database_permission_scriptTest_TestAction");
            // 
            // inconclusiveCondition37
            // 
            inconclusiveCondition37.Enabled = true;
            inconclusiveCondition37.Name = "inconclusiveCondition37";
            // 
            // system_generate_login_scriptTestData
            // 
            this.system_generate_login_scriptTestData.PosttestAction = null;
            this.system_generate_login_scriptTestData.PretestAction = null;
            this.system_generate_login_scriptTestData.TestAction = system_generate_login_scriptTest_TestAction;
            // 
            // system_generate_login_scriptTest_TestAction
            // 
            system_generate_login_scriptTest_TestAction.Conditions.Add(inconclusiveCondition38);
            resources.ApplyResources(system_generate_login_scriptTest_TestAction, "system_generate_login_scriptTest_TestAction");
            // 
            // inconclusiveCondition38
            // 
            inconclusiveCondition38.Enabled = true;
            inconclusiveCondition38.Name = "inconclusiveCondition38";
            // 
            // system_generate_secretTestData
            // 
            this.system_generate_secretTestData.PosttestAction = null;
            this.system_generate_secretTestData.PretestAction = null;
            this.system_generate_secretTestData.TestAction = system_generate_secretTest_TestAction;
            // 
            // system_generate_secretTest_TestAction
            // 
            system_generate_secretTest_TestAction.Conditions.Add(inconclusiveCondition39);
            resources.ApplyResources(system_generate_secretTest_TestAction, "system_generate_secretTest_TestAction");
            // 
            // inconclusiveCondition39
            // 
            inconclusiveCondition39.Enabled = true;
            inconclusiveCondition39.Name = "inconclusiveCondition39";
            // 
            // system_get_instance_tagTestData
            // 
            this.system_get_instance_tagTestData.PosttestAction = null;
            this.system_get_instance_tagTestData.PretestAction = null;
            this.system_get_instance_tagTestData.TestAction = system_get_instance_tagTest_TestAction;
            // 
            // system_get_instance_tagTest_TestAction
            // 
            system_get_instance_tagTest_TestAction.Conditions.Add(inconclusiveCondition40);
            resources.ApplyResources(system_get_instance_tagTest_TestAction, "system_get_instance_tagTest_TestAction");
            // 
            // inconclusiveCondition40
            // 
            inconclusiveCondition40.Enabled = true;
            inconclusiveCondition40.Name = "inconclusiveCondition40";
            // 
            // system_set_ag_agent_job_stateTestData
            // 
            this.system_set_ag_agent_job_stateTestData.PosttestAction = null;
            this.system_set_ag_agent_job_stateTestData.PretestAction = null;
            this.system_set_ag_agent_job_stateTestData.TestAction = system_set_ag_agent_job_stateTest_TestAction;
            // 
            // system_set_ag_agent_job_stateTest_TestAction
            // 
            system_set_ag_agent_job_stateTest_TestAction.Conditions.Add(inconclusiveCondition41);
            resources.ApplyResources(system_set_ag_agent_job_stateTest_TestAction, "system_set_ag_agent_job_stateTest_TestAction");
            // 
            // inconclusiveCondition41
            // 
            inconclusiveCondition41.Enabled = true;
            inconclusiveCondition41.Name = "inconclusiveCondition41";
            // 
            // system_set_default_agentjob_configurationTestData
            // 
            this.system_set_default_agentjob_configurationTestData.PosttestAction = null;
            this.system_set_default_agentjob_configurationTestData.PretestAction = null;
            this.system_set_default_agentjob_configurationTestData.TestAction = system_set_default_agentjob_configurationTest_TestAction;
            // 
            // system_set_default_agentjob_configurationTest_TestAction
            // 
            system_set_default_agentjob_configurationTest_TestAction.Conditions.Add(inconclusiveCondition42);
            resources.ApplyResources(system_set_default_agentjob_configurationTest_TestAction, "system_set_default_agentjob_configurationTest_TestAction");
            // 
            // inconclusiveCondition42
            // 
            inconclusiveCondition42.Enabled = true;
            inconclusiveCondition42.Name = "inconclusiveCondition42";
            // 
            // system_set_default_db_performance_configurationTestData
            // 
            this.system_set_default_db_performance_configurationTestData.PosttestAction = null;
            this.system_set_default_db_performance_configurationTestData.PretestAction = null;
            this.system_set_default_db_performance_configurationTestData.TestAction = system_set_default_db_performance_configurationTest_TestAction;
            // 
            // system_set_default_db_performance_configurationTest_TestAction
            // 
            system_set_default_db_performance_configurationTest_TestAction.Conditions.Add(inconclusiveCondition43);
            resources.ApplyResources(system_set_default_db_performance_configurationTest_TestAction, "system_set_default_db_performance_configurationTest_TestAction");
            // 
            // inconclusiveCondition43
            // 
            inconclusiveCondition43.Enabled = true;
            inconclusiveCondition43.Name = "inconclusiveCondition43";
            // 
            // system_get_hexadecimalTestData
            // 
            this.system_get_hexadecimalTestData.PosttestAction = null;
            this.system_get_hexadecimalTestData.PretestAction = null;
            this.system_get_hexadecimalTestData.TestAction = system_get_hexadecimalTest_TestAction;
            // 
            // system_get_hexadecimalTest_TestAction
            // 
            system_get_hexadecimalTest_TestAction.Conditions.Add(inconclusiveCondition44);
            resources.ApplyResources(system_get_hexadecimalTest_TestAction, "system_get_hexadecimalTest_TestAction");
            // 
            // inconclusiveCondition44
            // 
            inconclusiveCondition44.Enabled = true;
            inconclusiveCondition44.Name = "inconclusiveCondition44";
            // 
            // system_get_split_stringTestData
            // 
            this.system_get_split_stringTestData.PosttestAction = null;
            this.system_get_split_stringTestData.PretestAction = null;
            this.system_get_split_stringTestData.TestAction = system_get_split_stringTest_TestAction;
            // 
            // system_get_split_stringTest_TestAction
            // 
            system_get_split_stringTest_TestAction.Conditions.Add(inconclusiveCondition45);
            resources.ApplyResources(system_get_split_stringTest_TestAction, "system_get_split_stringTest_TestAction");
            // 
            // inconclusiveCondition45
            // 
            inconclusiveCondition45.Enabled = true;
            inconclusiveCondition45.Name = "inconclusiveCondition45";
            // 
            // checkmk_ag_role_change_datetimeTestData
            // 
            this.checkmk_ag_role_change_datetimeTestData.PosttestAction = null;
            this.checkmk_ag_role_change_datetimeTestData.PretestAction = null;
            this.checkmk_ag_role_change_datetimeTestData.TestAction = checkmk_ag_role_change_datetimeTest_TestAction;
            // 
            // checkmk_ag_role_change_datetimeTest_TestAction
            // 
            checkmk_ag_role_change_datetimeTest_TestAction.Conditions.Add(inconclusiveCondition46);
            resources.ApplyResources(checkmk_ag_role_change_datetimeTest_TestAction, "checkmk_ag_role_change_datetimeTest_TestAction");
            // 
            // inconclusiveCondition46
            // 
            inconclusiveCondition46.Enabled = true;
            inconclusiveCondition46.Name = "inconclusiveCondition46";
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void system_get_clean_stringTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_clean_stringTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_get_datetimeoffsetTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_datetimeoffsetTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_get_instance_guidTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_instance_guidTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_get_product_versionTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_product_versionTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void audit_get_database_last_accessTest()
        {
            SqlDatabaseTestActions testActions = this.audit_get_database_last_accessTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_chart_capacity_fgTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_chart_capacity_fgTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_agentjobTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_agentjobTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_alwaysonTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_alwaysonTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_backupTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_backupTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_databaseTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_databaseTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_integrityTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_integrityTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_logshippingTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_logshippingTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_check_mirroringTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_check_mirroringTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_inventory_agentjobTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_inventory_agentjobTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_inventory_alwaysonTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_inventory_alwaysonTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_inventory_databaseTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_inventory_databaseTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void collector_get_agentjob_historyTest()
        {
            SqlDatabaseTestActions testActions = this.collector_get_agentjob_historyTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void collector_get_backup_historyTest()
        {
            SqlDatabaseTestActions testActions = this.collector_get_backup_historyTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void collector_get_capacity_dbTest()
        {
            SqlDatabaseTestActions testActions = this.collector_get_capacity_dbTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void collector_get_database_ciTest()
        {
            SqlDatabaseTestActions testActions = this.collector_get_database_ciTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void collector_get_errorlog_historyTest()
        {
            SqlDatabaseTestActions testActions = this.collector_get_errorlog_historyTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void collector_get_instance_ciTest()
        {
            SqlDatabaseTestActions testActions = this.collector_get_instance_ciTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void datamart_process_get_dataTest()
        {
            SqlDatabaseTestActions testActions = this.datamart_process_get_dataTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CommandExecuteTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CommandExecuteTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DatabaseBackupTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DatabaseBackupTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DatabaseIntegrityCheckTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DatabaseIntegrityCheckTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_IndexOptimizeTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_IndexOptimizeTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_sp_WhoIsActiveTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_sp_WhoIsActiveTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void health_get_block_process_historyTest()
        {
            SqlDatabaseTestActions testActions = this.health_get_block_process_historyTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void health_get_dead_lock_historyTest()
        {
            SqlDatabaseTestActions testActions = this.health_get_dead_lock_historyTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void health_get_file_io_performanceTest()
        {
            SqlDatabaseTestActions testActions = this.health_get_file_io_performanceTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void health_get_index_fragmentationTest()
        {
            SqlDatabaseTestActions testActions = this.health_get_index_fragmentationTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void health_get_statistic_stateTest()
        {
            SqlDatabaseTestActions testActions = this.health_get_statistic_stateTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void health_get_transaction_logTest()
        {
            SqlDatabaseTestActions testActions = this.health_get_transaction_logTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_delete_system_historyTest()
        {
            SqlDatabaseTestActions testActions = this.system_delete_system_historyTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_execute_foreach_dbTest()
        {
            SqlDatabaseTestActions testActions = this.system_execute_foreach_dbTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_generate_database_permission_scriptTest()
        {
            SqlDatabaseTestActions testActions = this.system_generate_database_permission_scriptTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_generate_login_scriptTest()
        {
            SqlDatabaseTestActions testActions = this.system_generate_login_scriptTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_generate_secretTest()
        {
            SqlDatabaseTestActions testActions = this.system_generate_secretTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_get_instance_tagTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_instance_tagTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_set_ag_agent_job_stateTest()
        {
            SqlDatabaseTestActions testActions = this.system_set_ag_agent_job_stateTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_set_default_agentjob_configurationTest()
        {
            SqlDatabaseTestActions testActions = this.system_set_default_agentjob_configurationTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_set_default_db_performance_configurationTest()
        {
            SqlDatabaseTestActions testActions = this.system_set_default_db_performance_configurationTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_get_hexadecimalTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_hexadecimalTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void system_get_split_stringTest()
        {
            SqlDatabaseTestActions testActions = this.system_get_split_stringTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void checkmk_ag_role_change_datetimeTest()
        {
            SqlDatabaseTestActions testActions = this.checkmk_ag_role_change_datetimeTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions system_get_clean_stringTestData;
        private SqlDatabaseTestActions system_get_datetimeoffsetTestData;
        private SqlDatabaseTestActions system_get_instance_guidTestData;
        private SqlDatabaseTestActions system_get_product_versionTestData;
        private SqlDatabaseTestActions audit_get_database_last_accessTestData;
        private SqlDatabaseTestActions checkmk_chart_capacity_fgTestData;
        private SqlDatabaseTestActions checkmk_check_agentjobTestData;
        private SqlDatabaseTestActions checkmk_check_alwaysonTestData;
        private SqlDatabaseTestActions checkmk_check_backupTestData;
        private SqlDatabaseTestActions checkmk_check_databaseTestData;
        private SqlDatabaseTestActions checkmk_check_integrityTestData;
        private SqlDatabaseTestActions checkmk_check_logshippingTestData;
        private SqlDatabaseTestActions checkmk_check_mirroringTestData;
        private SqlDatabaseTestActions checkmk_inventory_agentjobTestData;
        private SqlDatabaseTestActions checkmk_inventory_alwaysonTestData;
        private SqlDatabaseTestActions checkmk_inventory_databaseTestData;
        private SqlDatabaseTestActions collector_get_agentjob_historyTestData;
        private SqlDatabaseTestActions collector_get_backup_historyTestData;
        private SqlDatabaseTestActions collector_get_capacity_dbTestData;
        private SqlDatabaseTestActions collector_get_database_ciTestData;
        private SqlDatabaseTestActions collector_get_errorlog_historyTestData;
        private SqlDatabaseTestActions collector_get_instance_ciTestData;
        private SqlDatabaseTestActions datamart_process_get_dataTestData;
        private SqlDatabaseTestActions dbo_CommandExecuteTestData;
        private SqlDatabaseTestActions dbo_DatabaseBackupTestData;
        private SqlDatabaseTestActions dbo_DatabaseIntegrityCheckTestData;
        private SqlDatabaseTestActions dbo_IndexOptimizeTestData;
        private SqlDatabaseTestActions dbo_sp_WhoIsActiveTestData;
        private SqlDatabaseTestActions health_get_block_process_historyTestData;
        private SqlDatabaseTestActions health_get_dead_lock_historyTestData;
        private SqlDatabaseTestActions health_get_file_io_performanceTestData;
        private SqlDatabaseTestActions health_get_index_fragmentationTestData;
        private SqlDatabaseTestActions health_get_statistic_stateTestData;
        private SqlDatabaseTestActions health_get_transaction_logTestData;
        private SqlDatabaseTestActions system_delete_system_historyTestData;
        private SqlDatabaseTestActions system_execute_foreach_dbTestData;
        private SqlDatabaseTestActions system_generate_database_permission_scriptTestData;
        private SqlDatabaseTestActions system_generate_login_scriptTestData;
        private SqlDatabaseTestActions system_generate_secretTestData;
        private SqlDatabaseTestActions system_get_instance_tagTestData;
        private SqlDatabaseTestActions system_set_ag_agent_job_stateTestData;
        private SqlDatabaseTestActions system_set_default_agentjob_configurationTestData;
        private SqlDatabaseTestActions system_set_default_db_performance_configurationTestData;
        private SqlDatabaseTestActions system_get_hexadecimalTestData;
        private SqlDatabaseTestActions system_get_split_stringTestData;
        private SqlDatabaseTestActions checkmk_ag_role_change_datetimeTestData;
    }
}
