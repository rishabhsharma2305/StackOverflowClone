using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModel;

namespace StackOverflowClone.RepositoryLayer
{
    public class AnswerRepo
    {

        ContextClass context;
        QuestionRepo questionRepo;
        public AnswerRepo()
        {
            context = new ContextClass();
            questionRepo = new QuestionRepo();
        }
        public void InsertAnswer(NewAnswerViewModel navm)
        {
            var config = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>();  });
            var mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(navm);
            questionRepo.UpdateQuestionAnswersCount(navm.QuestionID);
            //User user = context.Users.Where(u=>u.UserID==navm.UserID).FirstOrDefault();
            //a.User.UserName = user.UserName; 
            //int qid = navm.QuestionID;
            //Question question = context.Questions.Where(q => q.QuestionID==qid).FirstOrDefault();
            //question.Answers.Add(a);
            context.Answers.Add(a);
            context.SaveChanges();  
        }
        public void UpdateAnswer(EditAnswerViewModel eavm)
        {
            Answer answer = context.Answers.Where(a=>a.AnswerID == eavm.AnswerID).FirstOrDefault();
            if(answer != null)
            {
                answer.AnswerText = eavm.AnswerText;
                context.SaveChanges();
            }
        }
    }
}
