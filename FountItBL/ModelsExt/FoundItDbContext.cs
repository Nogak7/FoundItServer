﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FountItBL.Models;

public partial class FoundItDbContext : DbContext
{
    public async Task<ICollection<PostComment>> GetPostCommentsResponses( int postId)
    {


        //לשלוף את הפוסט ואת כל תגובות שלו

        var comments=await  this.PostComments.AsNoTracking().Where(pc => pc.Post == postId).OrderBy(pc=>pc.Date).ToListAsync();
        return comments;    
    }

    public async Task<ICollection<Post>> GetPostByUser(int userId)
    {
        var posts = await this.Posts.AsNoTracking().Where(p=> p.Creator == userId && p.Status!=4).Include(p=>p.StatusNavigation).Include(p=>p.CreatorNavigation).ToListAsync();    
        return posts;   
    }
 }