using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.BLL.Services;
using Doctors_Web_Forum.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doctors_Web_Forum.Web.Areas.Admin.Controllers
{
    public class TopicsController : Controller
    {
        private readonly TopicService _topicService;

        public TopicsController(TopicService topicService)
        {
            _topicService = topicService;
        }

        // GET : topics

        public async Task<IActionResult> Index()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            return View(topics);
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TopicName,Description,Status")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                await _topicService.AddTopicAsync(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }


        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicName,Description,Status")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _topicService.UpdateTopicAsync(topic);
                }
                catch (System.Exception)
                {
                    if (!await TopicExists(topic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }
        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _topicService.DeleteTopicAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TopicExists(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            return topic != null;
        }

    }
}
