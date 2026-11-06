using System;
using System.Collections.Generic;
using System.Text;
using Uniaraxa.Domain.Entities;
using Uniaraxa.Domain.Interfaces;

namespace Uniaraxa.Infra.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly List<Teacher> _teachers;

        public TeacherRepository()
        {
            _teachers = new List<Teacher>();
        }
        public void AddTeacher(Teacher teacher)
        {
            _teachers.Add(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var existingTeacher = GetTeacherById(teacher.Id);
            if(existingTeacher != null)
            {
                existingTeacher.Name = teacher.Name;
                existingTeacher.Email = teacher.Email;
                existingTeacher.urlLattes = teacher.urlLattes;
                existingTeacher.DataAdmissao = teacher.DataAdmissao;
            }
        }

        public void DeleteTeacher(int id)
        {
            var teacher = GetTeacherById(id);
            if(teacher != null)
            {
                _teachers.Remove(teacher);
            }
        }

        public Teacher GetTeacherById(int id)
        {
            return _teachers.Find(t => t.Id == id);
        }


        public List<Teacher> GetAllTeachers()
        {
            return _teachers;
        }
    }
}
