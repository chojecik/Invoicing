import { BooleanTextPipe } from './boolean-text.pipe';

describe('BooleanTextPipe', () => {
  it('create an instance', () => {
    const pipe = new BooleanTextPipe();
    expect(pipe).toBeTruthy();
  });
});
