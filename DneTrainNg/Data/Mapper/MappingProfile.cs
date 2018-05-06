using AutoMapper;
using DneTrainNg.Data.APIResources;
using DneTrainNg.Models;
using DneTrainNg.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseListResource>();
            CreateMap<Student, StudentCourse>()
                    .ForMember(sc=>sc.StudentCourseId, opt=>opt.Ignore());
            //.ForMember(sc=>);
            CreateMap<Student, StudentResource>()
                .ForMember(sr=>sr.Section, opt=>opt.MapFrom(s=>new SectionResource { SectionId=s.SectionId,SectionCode=s.SectionCode,SectionName=s.SectionName}))
                ;
            CreateMap<Course, CourseResource>()
                    .ForMember(cr=>cr.Students, opt=>opt.MapFrom(c=>c.StudentCourses.Select(sc=>sc.StudentId)));
            CreateMap<CourseChangeViewModel, Course>()
                    
                    //.ForMember(c => c.CourseName, opt => opt.MapFrom(vm => vm.CourseName))
                    //.ForMember(c => c.CourseStartDate, opt => opt.MapFrom(vm => vm.CourseStartDate))
                    //.ForMember(c => c.CourseEndDate, opt => opt.MapFrom(vm => vm.CourseEndDate))
                    //.ForMember(c => c.TrainHours, opt => opt.MapFrom(vm => vm.TrainHours))
                    .AfterMap((vm, c) =>
                    {
                        //Add new Student
                        foreach (var id in vm.Students)
                            if (!c.StudentCourses.Any(sc => sc.StudentId.Equals(id)))
                                c.StudentCourses.Add(new StudentCourse
                                {
                                    StudentId = id,
                                    //CourseName=course.CourseName,
                                    CourseId = c.CourseId,
                                    //Score = "N/A"
                                });


                        var removedStudents = new List<StudentCourse>();
                        foreach (var student in c.StudentCourses)
                            if (!vm.Students.Any(s => s.Equals(student.StudentId)))
                                removedStudents.Add(student);

                        foreach (var ToRemove in removedStudents)
                        {
                            c.StudentCourses.Remove(ToRemove);
                        }
                    });
        }
    }
}
