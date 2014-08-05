using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace TeamToyWPF.Data
{
    class ExcelManager
    {
        private const int USER_COLUNM_COUNT = 6;

        private DataManager mDataManager;
        private int mMonth;
        private string mFilePath;

        private Application mApplication;
        private Workbook mWorkbook;
        private Worksheet mWorksheet;

        public ExcelManager(DataManager dataManager, int month, string filePath)
        {
            mDataManager = dataManager;
            mMonth = month;
            mFilePath = filePath;
        }

        public bool execute()
        {
            try
            {
                mApplication = new Application();
                object Nothing = Missing.Value;
                mWorkbook = mApplication.Workbooks.Open(mFilePath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
                mApplication.DisplayAlerts = false;
                mApplication.AlertBeforeOverwriting = false;

                foreach (User user in mDataManager.mUsers)
                {
                    switch (user.name)
                    {
                        case "胡蓉":
                            saveSheet(user, 1);
                            break;
                        case "朱丽艳":
                            saveSheet(user, 1, 1);
                            break;
                        case "杨斌":
                            saveSheet(user, 2);
                            break;
                        case "焦磊":
                            saveSheet(user, 3);
                            break;
                        case "黄琛":
                            saveSheet(user, 3, 1);
                            break;
                        case "胡鹏飞":
                            saveSheet(user, 4);
                            break;
                        case "刘晓云":
                            saveSheet(user, 5);
                            break;
                        default:
                            break;
                    }
                }

                mWorkbook.Save();
                mApplication.Quit();
                mWorksheet = null;
                mWorkbook = null;
                mApplication = null;

                return true;
            }
            catch (Exception e)
            {
                new MessageManager().showError(e.Message);
                return false;
            }
        }

        private void saveSheet(User user, int sheetIndex, int userIndex = 0)
        {
            mWorksheet = mWorkbook.Sheets.get_Item(sheetIndex);
            List<ToDo> todoList = user.getValidatedToDoList(mMonth);
            for (int i = 0; i < todoList.Count; i++)
            {
                ToDo todo = todoList.ElementAt(i);
                mWorksheet.Cells[4 + i, 1 + userIndex * USER_COLUNM_COUNT] = DateTime.Now.Year + "." + todo.month + "." + todo.day;
                mWorksheet.Cells[4 + i, 2 + userIndex * USER_COLUNM_COUNT] = todo.content;
                Range contentRange = mWorksheet.Cells[4 + i, 2 + userIndex * USER_COLUNM_COUNT];
                contentRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                if (todo.isScoreSet)
                {
                    mWorksheet.Cells[4 + i, 3 + userIndex * USER_COLUNM_COUNT] = todo.score;
                }
                else
                {
                    mWorksheet.Cells[4 + i, 3 + userIndex * USER_COLUNM_COUNT] = "未评分";
                }
            }
        }

    }
}
