//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;
//using MvvmDemo.Models;
//using MvvmDemo.Commands;
//using System.Collections.ObjectModel;

//namespace MvvmDemo.ViewModels
//{
//    class EmployeeViewModel :INotifyPropertyChanged
//    {

//        //Proprty changed

//        public event PropertyChangedEventHandler PropertyChanged;
//        public void OnPropertyChanged(string propertyName)
//        {
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//        }
//        EmployeeService objEmployeeService;


//        public EmployeeViewModel()
//        {
//            objEmployeeService = new EmployeeService();
//            LoadData();
//            CurrentEmployee = new Employee();
//            saveCommand = new RelayCommand(Save);
//        }

//        private ObservableCollection<Employee> employeeList;

//        public ObservableCollection<Employee> EmployeeList
//        {
//            get { return employeeList; }
//            set { employeeList = value; OnPropertyChanged("EmployeeList"); }
//        }

//        private void LoadData()
//        {
//            EmployeeList = new ObservableCollection<Employee>(objEmployeeService.GetAll());
//            //EmployeeList = new ObservableCollection<Employee>(objEmployeeService.GetAll());
//        }


//        private Employee currentEmployee;

//        public Employee CurrentEmployee
//        {
//            get { return currentEmployee; }
//            set { currentEmployee = value;OnPropertyChanged("CurrentEmployee"); }
//        }

//        private string message;

//        public string Message
//        {
//            get { return message; }
//            set { message = value;OnPropertyChanged("Message"); }
//        }


//        private RelayCommand saveCommand;

//        public RelayCommand SaveCommand
//        {
//            get { return saveCommand; }

//        }

//        public void Save()
//        {
//            try
//            {
//                var IsSaved = objEmployeeService.Add(currentEmployee);
//                LoadData();
//                if (IsSaved)
//                    Message = "Employee Saved";
//                else
//                    Message = "Save Operation Failed";
//            }
//            catch(Exception ex)
//            {
//                Message = ex.Message;
//            }
//        }



//    }
//}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using MvvmDemo.Models;
using MvvmDemo.Commands;
using System.Collections.ObjectModel;
namespace MvvmDemo.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region DisplayOperation
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; OnPropertyChanged("EmployeesList"); }
        }
        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
        }
        #endregion

        #region SaveOperation
        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);
                if (ObjEmployee != null)
                {
                    CurrentEmployee = ObjEmployee;
                }
                else
                {
                    Message = "Employee not found";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
                if (IsUpdated)
                {
                    Message = "Employee updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region DeleteOperation
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeeService.Delete(CurrentEmployee.Id);
                if (IsDeleted)
                {
                    Message = "Employee deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}

