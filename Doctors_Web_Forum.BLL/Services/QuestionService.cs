using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.DAL.Data;
using Doctors_Web_Forum.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly DataDBContext _context;

        public QuestionService(DataDBContext context)
        {
            _context = context;
        }

        // Lấy thông tin người đăng câu hỏi và chủ đề (tái sử dụng cho nhiều phương thức)
        private IQueryable<Question> GetQuestionsWithRelatedEntities()
        {
            return _context.Questions
                .Include(q => q.User)  // Lấy thông tin người đăng câu hỏi
                .Include(q => q.Topic); // Lấy thông tin chủ đề câu hỏi
        }

        // Lấy tất cả câu hỏi
        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await GetQuestionsWithRelatedEntities().ToListAsync();
        }

        // Lấy câu hỏi theo Id
        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await GetQuestionsWithRelatedEntities()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        // Tạo câu hỏi mới
        public async Task<Question> CreateQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        // Cập nhật câu hỏi
        public async Task<Question> UpdateQuestionAsync(int id, string questionText, string description, int topicId)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return null; // Nếu câu hỏi không tồn tại
            }

            // Cập nhật các trường của câu hỏi
            question.QuestionText = questionText;
            question.Description = description;
            question.TopicId = topicId;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            return question;
        }

        // Kiểm tra sự tồn tại của câu hỏi trước khi xóa
        private async Task<Question> GetQuestionIfExistsAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        // Xóa câu hỏi
        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await GetQuestionIfExistsAsync(id);
            if (question == null)
            {
                return false; // Nếu câu hỏi không tồn tại
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
