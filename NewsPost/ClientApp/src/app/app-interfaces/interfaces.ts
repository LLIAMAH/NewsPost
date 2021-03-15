export interface IPost
{
  id: number;
  title: string;
  body: string;
}

export interface IPostActions {
  postAddId: string;
  postModalShow: () => void;
}
