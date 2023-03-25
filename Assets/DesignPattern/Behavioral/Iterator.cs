using System.Collections;
/// <summary>
/// 迭代器模式
/// </summary>
public class Iterator
{
    /// <summary>
    /// 用户列表
    /// </summary>
    /// <remarks>实现IEnumerable</remarks>
    public class UserList : IEnumerable
    {
        public string User1 = "AAA";
        public string User2 = "BBB";
        public string User3 = "CCC";


        public IEnumerator GetEnumerator()
        {
            //在C#中实现迭代器非常简单
            //使用yield，每次迭代返回一个yield return的值
            yield return User1;
            yield return User2;
            yield return User3;

            //也可以用在循环里
            string[] users = new string[] { User1, User2, User3 };
            foreach (string user in users)
            {
                yield return user;
            }
        }
    }

    /*
        调用方式：
        UserList userList = new UserList();
        foreach (string user in userList)
        {
            Console.WriteLine(user);
        }
     */
}
