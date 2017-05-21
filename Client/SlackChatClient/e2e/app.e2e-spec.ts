import { SlackChatClientPage } from './app.po';

describe('slack-chat-client App', () => {
  let page: SlackChatClientPage;

  beforeEach(() => {
    page = new SlackChatClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
