using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfreg.ViewModel
{

    internal class ChatInfoViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        public ChatInfoViewModel(ChatModel currentChat)
        {
            Title = currentChat.Name;
            Description = currentChat.Description;
        }
        public ChatInfoViewModel() { }
    }
}
