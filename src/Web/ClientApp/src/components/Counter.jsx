import { useState } from 'react';

export function Counter() {
  const [count, setCount] = useState(0);
  return (
    <section className="simple-page">
      <h1>Counter</h1>
      <p>Current count: <strong>{count}</strong></p>
      <button type="button" onClick={() => setCount(value => value + 1)}>Increment</button>
    </section>
  );
}
