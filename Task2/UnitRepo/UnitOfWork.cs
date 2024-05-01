using Task2.GenericRepo;
using Task2.Models;

namespace Task2.UnitRepo
{
    public class UnitOfWork
    {
        private readonly ITIContext _db;
        public UnitOfWork(ITIContext db)
        {
            _db = db;
        }

        private GenericRepo<Student> _studentRepo;
        public GenericRepo<Student> StudentRepo
        {
            get
            {
                if (_studentRepo == null)
                {
                    _studentRepo = new GenericRepo<Student>(_db);
                }
                return _studentRepo;
            }
        }

        private GenericRepo<Department> _departmentRepo;
        public GenericRepo<Department> DepartmentRepo
        {
            get
            {
                if (_departmentRepo == null)
                {
                    _departmentRepo = new GenericRepo<Department>(_db);
                }
                return _departmentRepo;
            }
        }

        private GenericRepo<Stud_Course> _studCourseRepo;
        public GenericRepo<Stud_Course> StudCourseRepo
        {
            get
            {
                if (_studCourseRepo == null)
                {
                    _studCourseRepo = new GenericRepo<Stud_Course>(_db);
                }
                return _studCourseRepo;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }   

    }
}

