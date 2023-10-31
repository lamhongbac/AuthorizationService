namespace AuthorizationService.Data
{
    public class RefreshTokenDatas
    {
        public RefreshTokenDatas()
        {
            RefreshTokens = new List<RefreshTokenData>();
        }
        public List<RefreshTokenData> RefreshTokens { get; set; }

        /// <summary>
        /// Add new token
        /// </summary>
        /// <param name="token"></param>
        public void AddToken(RefreshTokenData token)
        {
            int index = RefreshTokens.IndexOf(token);
            if (index==-1)
            {
                RefreshTokens.Add(token);
                //update vao mongo DB
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        public void RemoveToken(RefreshTokenData token)
        {
            int index = RefreshTokens.IndexOf(token);
            if (index>=0)
            {
                RefreshTokens.Remove(token);
                //update vao mongo DB
            }
        }

        public void Update(RefreshTokenData token)
        {
            RefreshTokenData oldTk=  RefreshTokens.FirstOrDefault(x => x.Id == token.Id); 
            if (oldTk!=null)
            {
                int index = RefreshTokens.IndexOf(oldTk);
                RefreshTokens[index] = token;
                //update vao mongo DB
            }
        }
    }
}
