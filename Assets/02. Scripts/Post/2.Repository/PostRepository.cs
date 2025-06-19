using System.Collections.Generic;
using System.Threading.Tasks;

public class PostRepository
{
    // Firebase에 저장된 데이터를 불러온다고 가정
    public async Task<List<PostDTO>> LoadAllPostsAsync()
    {
        // Firestore에서 최신순 정렬된 데이터 받아오기
        return await FirebasePostService.LoadAllPosts();
    }

    public async Task<PostDTO> LoadPostByIdAsync(string postId)
    {
        return await FirebasePostService.LoadPost(postId);
    }

    public async Task<bool> SavePostAsync(PostDTO post)
    {
        return await FirebasePostService.SavePost(post);
    }

    public async Task<bool> DeletePostAsync(string postId)
    {
        return await FirebasePostService.DeletePost(postId);
    }

    public async Task<bool> UpdatePostAsync(PostDTO post)
    {
        return await FirebasePostService.EditPost(post);
    }
}


// 게시글 삭제 -> 저장소에 대한 작업. 레포 or 매니저 책임
// 게시글 목록 보기 -> 포스트 여러개 불러오는 건 레포가 해야지