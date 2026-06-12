// namespace palindrome
// {
//     class PalinDromeNum {
//         static void Main (string[] args) {
//             string str = "madam";
//             bool isPalin = true;
//             for(int i=0; i < str.Length/2; i++)
//             {
//                 if(str[i] != str[str.Length - 1 - i])
//                 {
//                     isPalin = false;
//                     break;
//                 }
//             }
//             Console.WriteLine(isPalin ? "Palindrome" : "Not Palindrome");
//         }
//     }
    
// }
namespace palindrome
{
    class PalinDromeNum {
        static void Main (string[] args) {
            string str = "Shreyash";
            string strrev="";
            for(int i=str.Length -1; i >=0; i--)
            {
                strrev += str[i];
            }
            Console.WriteLine(strrev);
        }
    }
    
}