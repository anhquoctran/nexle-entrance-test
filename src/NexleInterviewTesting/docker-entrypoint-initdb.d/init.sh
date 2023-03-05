SQL_SCRIPT_PATH=/tmp/sql/init_script.sql

echo "########### Running SQL script against DB ###########"
mysql --user="root" --password="admin123@@" --database="entrance_test" <${SQL_SCRIPT_PATH}

echo "########### Script execution finished! ###########"
